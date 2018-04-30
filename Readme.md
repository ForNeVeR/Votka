Votka [![Build Status][badge-appveyor]][build-appveyor] [![Status Zero][status-zero]][andivionian-status-classifier]
=====

Votka is a .NET Standard library for parsing of [VTK file format][vtk-format].

Build
-----

[.NET Core 2.0 SDK][net-core-sdk] is required to build the project.

```console
$ dotnet build
```

Test
----

```console
$ dotnet test Votka.Tests
```

License
-------

For this project's source code and binaries license, see [License.md][license].
Some of the samples in [Votka.Tests/Data][test-data] directory's subdirectories
are licensed under their own terms. In that case, check `Readme.md` file in the
file directory.

[license]: License.md
[test-data]: Votka.Tests/Data

[andivionian-status-classifier]: https://github.com/ForNeVeR/andivionian-status-classifier#status-zero-
[build-appveyor]: https://ci.appveyor.com/project/ForNeVeR/votka/branch/master
[net-core-sdk]: https://www.microsoft.com/net/download/core#/sdk
[vtk-format]: https://www.vtk.org/wp-content/uploads/2015/04/file-formats.pdf

[badge-appveyor]: https://ci.appveyor.com/api/projects/status/saq32u215fuxfv83/branch/master?svg=true
[status-zero]: https://img.shields.io/badge/status-zero-lightgrey.svg
