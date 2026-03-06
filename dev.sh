echo "Running tailwindcss..."
tailwindcss -i ./MachineSystem/MachineSystem/wwwroot/app.css -o ./MachineSystem/MachineSystem/wwwroot/app.min.css --watch

echo "Starting dotnet watch..."
dotnet --watch