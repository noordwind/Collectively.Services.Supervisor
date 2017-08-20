#!/bin/bash
export ASPNETCORE_ENVIRONMENT=local
cd src/Collectively.Services.Supervisor
dotnet run --no-restore --urls "http://*:11001"