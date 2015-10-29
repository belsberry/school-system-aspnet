#!/bin/bash

pushd SchoolInformationSystem.Common
dnu build
popd


pushd SchoolInformationSystem.Models
dnu build
popd


pushd SchoolInformationSystem.Data
dnu build
popd


pushd SchoolInformationSystem.Web
dnu build
popd

pushd SchoolInformationSystem.DevSetup
dnu build
popd

pushd SchoolInformationSystem.UnitTests
dnu build
popd


