module Tests

open System
open System.Threading
open System.IO
open System.Reflection
open System.Runtime.InteropServices

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
    let output = reader.GetOutput()

    Assert.NotNull output
