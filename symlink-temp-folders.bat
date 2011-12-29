rd /S /Q r:\Temp\DotNetFlow

rd /S /Q src\DotNetFlow\bin
rd /S /Q src\DotNetFlow\obj
rd /S /Q src\DotNetFlow.Core\bin
rd /S /Q src\DotNetFlow.Core\obj
rd /S /Q src\DotNetFlow.Features\bin
rd /S /Q src\DotNetFlow.Features\obj
rd /S /Q src\DotNetFlow.Migrations\bin
rd /S /Q src\DotNetFlow.Migrations\obj
rd /S /Q src\DotNetFlow.Specifications\bin
rd /S /Q src\DotNetFlow.Specifications\obj
rd /S /Q src\DotNetFlow.Utils\bin
rd /S /Q src\DotNetFlow.Utils\obj

mkdir r:\Temp\DotNetFlow
mkdir r:\Temp\DotNetFlow\Web\bin
mkdir r:\Temp\DotNetFlow\Web\obj
mkdir r:\Temp\DotNetFlow\Core\bin
mkdir r:\Temp\DotNetFlow\Core\obj
mkdir r:\Temp\DotNetFlow\Features\bin
mkdir r:\Temp\DotNetFlow\Features\obj
mkdir r:\Temp\DotNetFlow\Migrations\bin
mkdir r:\Temp\DotNetFlow\Migrations\obj
mkdir r:\Temp\DotNetFlow\Specifications\bin
mkdir r:\Temp\DotNetFlow\Specifications\obj
mkdir r:\Temp\DotNetFlow\Utils\bin
mkdir r:\Temp\DotNetFlow\Utils\obj

mklink /D src\DotNetFlow\bin r:\Temp\DotNetFlow\Web\bin
mklink /D src\DotNetFlow\obj r:\Temp\DotNetFlow\Web\obj

mklink /D src\DotNetFlow.Core\bin r:\Temp\DotNetFlow\Core\bin
mklink /D src\DotNetFlow.Core\obj r:\Temp\DotNetFlow\Core\obj

mklink /D src\DotNetFlow.Features\bin r:\Temp\DotNetFlow\Features\bin
mklink /D src\DotNetFlow.Features\obj r:\Temp\DotNetFlow\Features\obj

mklink /D src\DotNetFlow.Migrations\bin r:\Temp\DotNetFlow\Migrations\bin
mklink /D src\DotNetFlow.Migrations\obj r:\Temp\DotNetFlow\Migrations\obj

mklink /D src\DotNetFlow.Specifications\bin r:\Temp\DotNetFlow\Specifications\bin
mklink /D src\DotNetFlow.Specifications\obj r:\Temp\DotNetFlow\Specifications\obj

mklink /D src\DotNetFlow.Utils\bin r:\Temp\DotNetFlow\Utils\bin
mklink /D src\DotNetFlow.Utils\obj r:\Temp\DotNetFlow\Utils\obj