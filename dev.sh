echo "Running tailwindcss..."
yarn dlx @tailwindcss/cli -i ./MachineSystem/MachineSystem/wwwroot/app.css -o ./MachineSystem/MachineSystem/wwwroot/app.min.css --watch[=always]
TAILWIND_PID=$!

cleanup() {
    echo "Stopping tailwindcss..."
    kill $TAILWIND_PID 2>/dev/null
}
trap cleanup EXIT

echo "Starting dotnet watch..."
dotnet watch --project MachineSystem/MachineSystem