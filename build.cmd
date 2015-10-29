pushd SchoolInformationSystem.Common
call dnu build
popd
pushd SchoolInformationSystem.Models
call dnu build
popd
pushd SchoolInformationSystem.Data
call dnu build
popd
pushd SchoolInformationSystem.Web
call dnu build
popd
pushd SchoolInformationSystem.DevSetup
call dnu build
popd
pushd SchoolInformationSystem.UnitTests
call dnu build
popd
