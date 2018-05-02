module internal Votka.TextParser

open System
open System.Text.RegularExpressions

open Votka

let private error msg ([<ParamArray>] args) =
    Error (sprintf msg args)

let private parseRegex regex resultCreator =
    let regex = Regex(regex, RegexOptions.Compiled)
    fun line ->
        let m = regex.Match(line)
        if m.Success
        then resultCreator m.Groups
        else error "Cannot parse string %s" line

let parseHeaderVersion : string -> Result<Version, string> =
    parseRegex @"# vtk DataFile Version (\d+.\d+)" (fun groups -> Ok(Version(groups.[0].Value)))

let parseDatasetFormat : string -> Result<unit, string> =
    parseRegex "TEXT" (fun _ -> Ok()) // TODO[F]: support binary

let parseDatasetType : string -> Result<DatasetType, string> =
    parseRegex @"DATASET (\w+)" (fun groups ->
        match groups.[0].Value with
        | "STRUCTURED_POINTS" -> Ok StructuredPoints
        | "STRUCTURED_GRID" -> Ok StructuredGrid
        | "RECTILINEAR_GRID" -> Ok RectilinearGrid
        | "POLYDATA" -> Ok PolyData
        | "UNSTRUCTURED_GRID" -> Ok UnstructuredGrid
        | "FIELD" -> Ok Field
        | other -> error "Unknown dataset type name: %s" other)

let parsePointsHeader : string -> Result<PointsHeader, string> =
    parseRegex @"POINTS (\d+) (\w+)" (fun groups ->
        let number = int(groups.[0].Value)
        let dataType =
            match groups.[1].Value with
            | "bit" -> Ok Bit
            | "unsigned_char" -> Ok UnsignedChar
            | "char" -> Ok Char
            | "unsigned_short" -> Ok UnsignedShort
            | "short" -> Ok Short
            | "unsigned_int" -> Ok UnsignedInt
            | "int" -> Ok Int
            | "unsigned_long" -> Ok UnsignedLong
            | "long" -> Ok Long
            | "float" -> Ok Float
            | "double" -> Ok Double
            | other -> error "Unknown data type: %s" other
        dataType
        |> Result.map (fun dt ->
            { number = number
              dataType = dt }))
