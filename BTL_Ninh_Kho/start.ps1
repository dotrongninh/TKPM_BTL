# Start the .NET application
Start-Process -NoNewWindow -FilePath "dotnet" -ArgumentList "run"

# Wait for the server to start (adjust the time if needed)
Start-Sleep -Seconds 5

# Open Chrome with the application URL
Start-Process -FilePath "chrome" -ArgumentList "http://localhost:5000" 