
function Assert{
	param (
		$command,
		$in,
		$want
	)
	
	$bump = ".\Netsoft.Tools.Bump\bin\Release\netcoreapp3.0\dotnet-bump.exe"

	$got = & $bump $command $in

	if($got -ne $want){
		Write-Error("
	Got :$got
	Want:$want")
	}
	else{
		Write-Host("OK: ${command} ${in} => ${want}")
	}
}

function Assert-Error{
	param (
		$command,
		$in,
		$want
	)
	
	$bump = ".\Netsoft.Tools.Bump\bin\Release\netcoreapp3.0\dotnet-bump.exe"

	$got = & $bump $command $in 2>&1

	if(!("$got".Equals("$want"))){
		Write-Error("
	Got :$got
	Want:$want")
	}
	else{
		Write-Host("OK: ${command} ${in} => ${want}")
	}
}

function Run {
	dotnet test /p:configuration="Release"

	Assert-Error "major" "1"	"Version string was less than 2 digits."
	Assert-Error "major" "1.x"	"Input string was not in a correct format."

	Assert "major" "1.2.3" "2.0.0.0"
	Assert "minor" "1.2.3" "1.3.0.0"
	Assert "patch" "1.2.3" "1.2.4.0"
}

Run
