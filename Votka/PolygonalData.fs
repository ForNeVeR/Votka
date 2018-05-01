namespace Votka

[<Struct>]
type Point<'T> = Point of x : 'T * y : 'T * z : 'T

type PointIndexSequence = int[]

type PolygonalData<'DataType> =
    { points : Point<'DataType>[]
      vertices : PointIndexSequence
      lines : PointIndexSequence
      polygons : PointIndexSequence
      triangleStrips : PointIndexSequence }

module PolygonalData =
    let empty =
        { points = Array.empty
          vertices = Array.empty
          lines = Array.empty
          polygons = Array.empty
          triangleStrips = Array.empty }
