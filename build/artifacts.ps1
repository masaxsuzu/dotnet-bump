function Run {
	
	dotnet build -c Release

	StopIfError "Build error"

	dotnet test -c Release

	StopIfError "Test error"

	dotnet pack --output pkg -c Release --no-build

	StopIfError "Package error"

}

function StopIfError{
	param(
		$message
	)

	if($?)
	{
		return
	}

	Write-Error $message
}

$ErrorActionPreference = "Stop"
Run
