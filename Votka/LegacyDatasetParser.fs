module Votka.LegacyDatasetParser

open System.IO
open System.Text.RegularExpressions

open Votka
open Votka.StreamSupport

let private parsePoints reader : Async<IPointCollection> =
    async {
        let! header = parseLine reader TextParser.parsePointsHeader "Cannot parse points header: %s"
        let! collection = PointParser.parse header reader
        return collection
    }

let private parseContent reader = function
| PolyData ->
    async {
        let! points = parsePoints reader
        let! (vertices, lines, polygons, triangleStrips) =
            async { return Array.empty, Array.empty, Array.empty, Array.empty }
            // TODO: parseSections reader [| "VERTICES", "LINES", "POLYGONS", "TRIANGLE_STRIPS" |]
        return
            { points = points
              vertices = vertices
              lines = lines
              polygons = polygons
              triangleStrips = triangleStrips }
    }
| other -> failwithf "Cannot parse dataset of type %A" other

let read (stream : Stream) : Async<Dataset> =
    async {
        use reader = new StreamReader(stream)
        let! headerLine = await <| reader.ReadLineAsync()
        let! version = parseLine reader TextParser.parseHeaderVersion "Cannot parse file header: %s"
        let! title = readLine reader
        let! format = parseLine reader TextParser.parseDatasetFormat "Cannot parse dataset format: %s"
        let! datasetType = parseLine reader TextParser.parseDatasetType "Cannot parse dataset type: %s"
        let! content = parseContent reader datasetType
        return
            { version = version
              title = title
              content = content }
    }
