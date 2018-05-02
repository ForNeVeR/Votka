module Votka.Tests.Activiz.Tests

open System
open System.IO
open System.Reflection

open System.IO
open Kitware.VTK
open Xunit

open Votka

let private getDataFilePath filePath =
    let directory =
        Assembly.GetExecutingAssembly().Location
        |> Path.GetDirectoryName
    Path.Combine(directory, filePath)

let private getVtkDataPath fileName =
    getDataFilePath(Path.Combine("Data/VTKData/Data", fileName))

let private readPolyDataActiviz path =
    use reader = vtkDataSetReader.New()
    reader.SetFileName path
    reader.Update()

    let output = reader.GetOutput()
    let content =
        output
        |> vtkPolyData.SafeDownCast
        |> Utils.getPolygonalData
    { version = Version(2, 0)
      title = reader.GetHeader()
      content = content }

let ``hello.vtk parsing results should be analogous`` () =
    let filePath = getVtkDataPath "hello.vtk"
    let expectedData = readPolyDataActiviz filePath
    let actualData =
        use stream = new FileStream(filePath, FileMode.Open)
        LegacyDatasetParser.read stream
        |> Async.RunSynchronously

    Assert.Equal(expectedData, actualData)
