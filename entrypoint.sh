#!/bin/sh

dotnet ef database update

exec dotnet simpleReading.dll