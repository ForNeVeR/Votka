module internal Votka.StreamSupport

open System.IO

let await = Async.AwaitTask
let readLine (reader : StreamReader) = await <| reader.ReadLineAsync()
let parseLine (reader : StreamReader) parseFn errorTemplate =
    async {
        let! line = readLine reader
        return
            match parseFn line with
            | Error x -> failwithf errorTemplate x
            | Ok result -> result
    }
