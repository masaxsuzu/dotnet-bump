function Run {
	
	dotnet test /p:configuration=Release

	StopIfError "Test error"

	dotnet pack --output pkg /p:configuration=Release --no-build

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
