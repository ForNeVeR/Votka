namespace Votka

open System

[<Struct>]
type Point<'T> = Point of x : 'T * y : 'T * z : 'T

type PointIndexSequence = int[]

type IPointCollection =
    abstract member GetPoint<'R> : int -> Point<'R>

type PointCollection<'T>(points : Point<'T>[]) =
    member __.Points = points
    member __.GetTypedPoint(index : int) : Point<'T> = points.[index]
    interface IPointCollection with
        member this.GetPoint<'R>(index : int) : Point<'R> =
            if typeof<'R> = typeof<'T>
            then (box <| this.GetTypedPoint index) :?> Point<'R>
            else
                let (Point(x, y, z)) = points.[index]
                let convert a = Convert.ChangeType(a, typeof<'R>) :?> 'R
                Point(convert x, convert y, convert z)

module PointCollection =
    let empty<'a> = PointCollection(Array.empty<Point<'a>>)

type PolygonalData =
    { points : IPointCollection
      vertices : PointIndexSequence
      lines : PointIndexSequence
      polygons : PointIndexSequence
      triangleStrips : PointIndexSequence }

module PolygonalData =
    let empty<'a> =
        { points = PointCollection.empty<'a>
          vertices = Array.empty
          lines = Array.empty
          polygons = Array.empty
          triangleStrips = Array.empty }
