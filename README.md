# Chaos Family Decryptors by Truesec

## Target .NET Framework 4.7.2
Use Visual Studio 2022 and open the Truesec.Decryptors.netfx.sln

If build fails with error claiming "Your project does not reference..." make sure to remove the /bin and /obj directories in the project root.

## Target .NET Core 6.0
Build with dotnet with the following command

```
dotnet run --project ./Truesec.decryptors.csproj
```

## Running tests
Build and run the tests with the following command

To test with actual files you need some malware encrypted files placed in the following directories:
```
Tests
  +--Files
       +--Chaos
       +--Onyx2
       +--Solidbit
```
The test has hardcoded values for the expected results and there actually tests that also test recursive functionality which requires files to be placed in subfolders as well.

```
dotnet test .\Tests\Tests.csproj
```