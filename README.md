
  # Restaurant Api  
  Repositorio para la gestion de ventas, ordénes y sus usuarios dentro del restaurante. 

  
## Ejecución local 
Clonar el repositorio 

~~~bash  
  git clone https://github.com/AugustoSan/RestaurantApi.git
~~~

Go to the project directory  

~~~bash  
  cd RestaurantApi
~~~

Compilar el proyecto

~~~bash  
dotnet build
~~~

Ejecutar el proyecto

~~~bash  
dotnet run --project src/Restaurant.Api/Restaurant.Api.csproj 
~~~  

Para visitar swagger(http://localhost:5035/swagger/index.html)

## Ejecucion con docker

Creación de la imagen docker

~~~bash  
docker buildx build -t restaurantapi:latest .
~~~  

Deploguear la imagen docker con docker compose con mongo y el volumen

~~~bash  
docker compose up -d
~~~  

Deploguear solo la imagen

~~~bash  
docker compose up api -d
~~~  