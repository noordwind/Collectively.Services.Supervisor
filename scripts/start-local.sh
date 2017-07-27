#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Collectively.Services.Supervisor
dotnet run --urls "http://*:11001"