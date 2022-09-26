cd "${0%/*}" || exit 1
dotnet build ./src/TwitterLib.csproj
dotnet build ./tests/TwitterLibTests.csproj