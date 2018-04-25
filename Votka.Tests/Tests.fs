module Votka.Tests

open System

open Xunit

open Votka

[<Fact>]
let ``Say should return unit`` () =
    Assert.Equal((), Say.hello "Paul")
