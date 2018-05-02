module Votka.Tests.Activiz.Utils

open System
open System.Runtime.InteropServices

open Kitware.VTK

open Votka

let initializeActivizErrorHandler() : unit =
    let errorHandler = vtkObject.vtkObjectEventHandler(fun x y ->
        failwithf "Error from vtkOutputWindow: %s" (Marshal.PtrToStringAnsi(y.CallData)))

    let errorWindow = vtkOutputWindow.GetInstance()
    errorWindow.add_ErrorEvt errorHandler

    printfn "vtkOutputWindow error handler initialized"

let private getPoint (dataSet : vtkDataSet) index =
    let array = dataSet.GetPoint (int64 index)
    Point(array.[0], array.[1], array.[2])

let private getDataValue (data : vtkIdTypeArray) index =
    data.GetValue(int64 index)

let getPolygonalData (dataSet : vtkPolyData) : PolygonalData =
    let pointCount = Checked.int <| dataSet.GetNumberOfPoints()

    let points =
        Seq.init pointCount (getPoint dataSet)
        |> Seq.toArray

    let lineCount = Checked.int <| dataSet.GetNumberOfLines()
    let lineData = dataSet.GetLines().GetData()
    let lines =
        Seq.init lineCount (getDataValue lineData >> int)
        |> Seq.toArray

    { PolygonalData.empty with
        points = PointCollection points
        lines = lines }


