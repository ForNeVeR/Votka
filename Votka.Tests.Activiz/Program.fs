module Votka.Tests.Activiz.Program

open System

let private testMethods = [|
    Tests.``hello.vtk parsing results should be analogous``
|]

let private runTest index method =
    printf "Running test %d of %d... " (index + 1) testMethods.Length
    try
        method()
        printfn "Ok"
        None
    with
    | error ->
        printfn "Error: %s" error.Message
        Some error

let private reportErrors errors =
    errors
    |> Array.choose id
    |> Array.iter (printfn "%A\n")
    errors

let private getExitCode errors =
    if Array.exists Option.isSome errors
    then eprintfn "Errors occured!"; -1
    else printfn "All tests ok!"; 0

[<EntryPoint>]
let main (_ : string[]) : int =
    Utils.initializeActivizErrorHandler()
    Array.mapi runTest testMethods
    |> reportErrors
    |> getExitCode
