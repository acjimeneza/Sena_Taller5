# SENA taller número 5

## Creación de la imagen base backend

El primer paso es la construcción de la imagen base, para esto se deben ejecutar los siguientes comandos:

```
cd 1-Imagen-Backend
docker build -f .docker/Dockerfile -t nombre-usuario/ms-sena:2 .
```
docker build -f .docker/Dockerfile -t nombre-usuario/ms-sena:2 .
Si se desea validar la imagen local, el usuario debe ejecutar la imagen creada en docker:

```
docker run -p 5000:5000 -d nombre-usuario/ms-sena:2
```

Luego, se debe abrir el navegador y ejecutar la siguiente URL para ingresar al swagger `http://localhost:5000/swagger`

Para bublicar la imagen en nuestros dockerHub se deben ejecutar los siguientes comandos:

```
docker login # Desde la consola ingresar el nombre de usuario y la contraseña
docker push nombre-usuario/ms-sena:2
```

## Creación de la imagen base mysql

Para inciar con el manejo de persistencia usando volumenes, es interesante iniciar con una imagen que necesite de persistencia para el almacenamiento de la información. Para este caso, se hace uso de una imagen de MySQL.

Para crear esta imagen, es necesario estar en la carpeta `1-Imagen-Backend` y ejecutar los siguientes comandos:

```
docker build -f .docker/DockerfileMySql -t nombre-usuario/mysql:1 .
docker push nombre-usuario/mysql:1
```

En caso de querer validar la imagen de MySQL de forma local, se recomienda ejecutar el siguiente comando:

```
docker run -it --rm -p 3306:3306 --name mysql -e MYSQL_ROOT_PASSWORD=1234 -e MYSQL_USER=admin -e MYSQL_PASSWORD=1234 -e MYSQL_DATABASE=Timedb nombre-usuario/mysql:1
```

## Creación de la imagen base frontend

El Segundo paso es la construcción de la imagen base para el front, para esto se deben ejecutar los siguientes comandos:

```
cd 2-Imagen-Frontend
docker build -f .docker/Dockerfile -t nombre-usuario/front-sena:2 .
```
docker build -f .docker/Dockerfile -t nombre-usuario/front-sena:2 .
Si se desea validar la imagen local, el usuario debe ejecutar la imagen creada en docker:

```
docker run -p 4200:4200 -d nombre-usuario/front-sena:2
```

Luego, se debe abrir el navegador  `http://localhost:4200`

Para bublicar la imagen en nuestros dockerHub se deben ejecutar los siguientes comandos:

```
docker login # Desde la consola ingresar el nombre de usuario y la contraseña
docker push nombre-usuario/front-sena:2
```


##  Conexión servicio front con el backend dentro de K8s

### Creación servicio loadbalancer y clusterip


Desde la raíz del repositorio, ingresar a la carpeta:

```
cd 3-Carga-Frontend
```
Luego se debe ejecutar el siguiente comando, para la creación del servicio y el despliegue:

```
kubectl apply -f frontend.yml
```
Validar que se haya creado el servicio de manera correcta:

```
kubectl get svc
```

## Creación de la base de datos

### Configmap

Desde la raíz del repositorio, ingresar a la carpeta:

```
cd 4-Volumenes
```

Luego se debe ejecutar los siguientes comandos, para la creación del configmap:

```
kubectl apply -f config.yml
```

### Cración del despliegue sin persistencia

Desde la raíz del repositorio, ingresar a la carpeta:

```
cd 4-Volumenes
```

Y ejecutar

```
kubectl apply -f deploy.yml
```

No olvidar elminiar los recursos creados con el comando `kubectl delete -f deploy.yml`


### Cración del despliegue con persistencia

Desde la raíz del repositorio, ingresar a la carpeta:

```
cd 4-Volumenes/Persistance
```

Y ejecutar

```
kubectl apply -f persistentVolume.yml
```

Este creara los volumenes para el manejo de la persistencia, esperemos que se hayan configurado de manera correcta, esto se puede hacer con el siguiente comando:

```
kubectl get pv
kubectl get pvc
```

Al validar el correcto despliegue de los volumenes, se debe crear el despliegue con la configuración especifica 

```
kubectl apply -f persistentDeploy.yml
```

Luego del despliegue puden que encuentren errores a la hora de la conexión, esto se debe a que la configuración dentro de la imagen se sobre pone al hacer el montaje del volumen, para lo cual, se deben ejecutar los siguientes comandos:

```
kubectl exec -it pod/mysql-deploy-0 -- bin/bash

#root> mysql -u root -p
password:<vacio>

mysql>
```

Ya dentro de MySQL, se debe ejecutar el siguiente script, el cual puede variar dependiendo si se realizaron cambos en las configuracionesL

```sql
CREATE DATABASE Timedb;

CREATE USER 'admin'@'%' IDENTIFIED BY '1234';

CREATE TABLE Timedb.TimeData (
id INT UNSIGNED AUTO_INCREMENT PRIMARY KEY,
Date DATETIME NOT NULL,
Number INT  NOT NULL);

GRANT ALL PRIVILEGES ON * . * TO 'admin'@'%';
```

El anterior script se encuentra en `./4-Volumenes/Persistance/initSql.sql`

Ya con esto se deberia poder observar el correcto funcionamiento de la aplicación.

No olvidar elminiar los recursos creados con el comando `kubectl delete -f persistentVolume.yml` y `kubectl delete -f persistentDeploy.yml`