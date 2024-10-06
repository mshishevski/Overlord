# Prerequisites: 


# Variables
$remoteUser = "shishevski"
$remoteHost = "shishevskipi"
$remotePath = "/home/$remoteUser/apps/Overlord"
$localPublishPath = "C:/Repositories/Overlord/Overlord.Api/bin/Release/net6.0/linux-arm64/publish"


# Function to execute remote SSH commands
function Invoke-SSHCommand {
    param (
        [string]$command
    )
    # Execute the SSH command and capture the output
    $output = ssh "$remoteUser@$remoteHost" ${command}

    # Print the output to the console
    if ($output) {
        Write-Host "Output from SSH command:" -ForegroundColor Green
        Write-Host $output
    } else {
        Write-Host "No output from command." -ForegroundColor Yellow
    }
}

# Step 1: Kill the existing process if it exists
Invoke-SSHCommand "pkill -f Overlord.Api"

# Step 2: Create directory for deypoyment
Invoke-SSHCommand "mkdir ./apps"

# Step 3: Remove the contents of the folder
Invoke-SSHCommand "rm -r $remotePath/*"

# Step 4: Build the .NET solution on the Windows machine
dotnet publish -r linux-arm64 -c Release --self-contained

# Step 5: Copy the build output to the remote Linux machine
scp -r "$localPublishPath" $remoteUser@${remoteHost}:${remotePath}

# Step 6: Update database with latest migrations

# Step 7: Start the service on the Linux machine
Invoke-SSHCommand "nohup dotnet ${remotePath}/publish/Overlord.Api.dll > /dev/null 2>&1 &"

