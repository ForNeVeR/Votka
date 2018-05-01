module Votka.Tests.Activiz.Utils

open System.Runtime.InteropServices

open Kitware.VTK

let initializeActivizErrorHandler() =
    let errorHandler = vtkObject.vtkObjectEventHandler(fun x y ->
        failwithf "Error from vtkOutputWindow: %s" (Marshal.PtrToStringAnsi(y.CallData)))

    let errorWindow = vtkOutputWindow.GetInstance()
    errorWindow.add_ErrorEvt errorHandler

    printfn "vtkOutputWindow error handler initialized"
