# Limpiar caché y recursos antiguos
Write-Host "🧹 Limpiando caché de Docker..."
docker builder prune -a -f
docker system prune -a -f

# Eliminar imagen previa si existe
Write-Host "🗑️ Eliminando imagen anterior..."
docker rmi gestionvehiculos -f

# Reconstruir imagen desde cero
Write-Host "⚙️ Construyendo nueva imagen..."
docker build --no-cache -t gestionvehiculos .

# Ejecutar contenedor con nombre fijo
Write-Host "🚀 Ejecutando contenedor..."
docker run -d -p 5000:80 --name vehiculos gestionvehiculos

# Mostrar lista de contenedores activos
Write-Host "📋 Contenedores activos:"
docker ps

# Mostrar logs iniciales del contenedor
Write-Host "📄 Logs del contenedor vehiculos:"
docker logs vehiculos
