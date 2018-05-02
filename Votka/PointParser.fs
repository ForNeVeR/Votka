module internal Votka.PointParser

open System.IO

open Votka.StreamSupport

type private CollectionGenerator =
    { result : IPointCollection
      add : string -> unit }

let private getGenerator<'T> parser count =
    let array = Array.zeroCreate count
    let collection = PointCollection<'T> array
    let mutable index = 0
    let adder = fun (line : string) ->
        let data = line.Split ' ' |> Array.map parser
        let point = Point(data.[0], data.[1], data.[2])
        array.[index] <- point
    { result = collection
      add = adder }

let private parseBool = function
| "0" -> false
| "1" -> true
| other -> failwithf "Cannot parse bool value: %s" other

let private createCollection : DataType -> int -> CollectionGenerator = function
| Bit -> getGenerator parseBool
| UnsignedChar -> getGenerator byte
| Char -> getGenerator sbyte
| UnsignedShort -> getGenerator int
| Short -> getGenerator int16
| UnsignedInt -> getGenerator uint16
| Int -> getGenerator int
| UnsignedLong -> getGenerator uint64
| Long -> getGenerator int64
| Float -> getGenerator float32
| Double -> getGenerator float

let parse (header : PointsHeader) (reader : StreamReader) : Async<IPointCollection> =
    async {
        let collection = createCollection header.dataType header.number
        for _ in 1 .. header.number do
            let! line = readLine reader
            collection.add line

        return collection.result
    }

