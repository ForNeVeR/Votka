module Votka.Tests.Activiz.Tests

open System
open System.IO
open System.Reflection

open Kitware.VTK
open Xunit

let private getDataFilePath filePath =
    let directory =
        Assembly.GetExecutingAssembly().Location
        |> Path.GetDirectoryName
    Path.Combine(directory, filePath)

let private getVtkDataPath fileName =
    getDataFilePath(Path.Combine("Data/VTKData/Data", fileName))

let ``hello.vtk parsing results should be analogous`` () =
    use reader = vtkDataSetReader.New()
    reader.SetFileName(getVtkDataPath "hello.vtk")
    reader.Update()
    let output = vtkPolyData.SafeDownCast <| reader.GetOutput()
    let polygonalData = Utils.getPolygonalData output

    printfn "%A" polygonalData
    Assert.NotNull polygonalData
