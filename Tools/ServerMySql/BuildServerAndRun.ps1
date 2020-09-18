##Copiar ficheros de la base de datos
$source = "src/Database"
$destino = "Tools/ServerMysql"

Copy-Item -Path $source -Filter "*.sql" -Recurse -Destination $destino -Container -force

##Borrar la imagen vieja
docker rm $(docker stop $(docker ps -a -q --filter ancestor='server-mysql' --format="{{.ID}}"))



##construir la imagen
docker build -t server-mysql Tools\ServerMysql\.

##iniciar el contenedor
docker run -d -p 4306:3306 server-mysql