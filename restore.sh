
pushd SchoolInformationSystem.Common
dnu restore
popd


pushd SchoolInformationSystem.Models
dnu restore
popd


pushd SchoolInformationSystem.Data
dnu restore
popd


pushd SchoolInformationSystem.Web
dnu restore
popd

pushd SchoolInformationSystem.DevSetup
dnu restore
popd

pushd SchoolInformationSystem.UnitTests
dnu restore
popd
