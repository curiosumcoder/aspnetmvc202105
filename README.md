# aspnetmvc202105
Material relacionado con el curso de ASP.NET MVC Web Applications (.NET Framework) impartido para Babel Learning (modalidad virtual) en Mayo 2020.

### Requerimientos de hardware y software
* Requisitos mínimos del hardware que ocupamos. 
	https://docs.microsoft.com/en-us/visualstudio/releases/2019/system-requirements
	
* Microsoft SQL Server 2008 R2 o superior. 
	https://www.microsoft.com/en-us/sql-server/sql-server-downloads
	Se acostumbra a utilizar la edición Express, en SQL Server 2017 para desarrollo es posible utilizar la edición Developer.
	
	Incluir la instalación del SQL Server Management Studio.
	https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15
	
* Microsoft Visual Studio 2019 (edición Community o superior) 
	https://visualstudio.microsoft.com/downloads/
	https://docs.microsoft.com/en-us/visualstudio/install/install-visual-studio?view=vs-2019#install-workloads
	
	Aquí se documenta como obtener los instaladores para la instalación local, 
	https://docs.microsoft.com/en-us/visualstudio/install/create-a-network-installation-of-visual-studio?view=vs-2019, 
	esto baja todos los “workloads” pero al momento de instalar no se deben de instalar todos.
	
	Si el Visual Studio 2019 ya se encuentra instalado se puede utilizar el Visual Studio Installer, 
	para efectuar la actualización.

	Se deben instalar al menos los “workloads”: 
	- Desktop & Mobile 
		+ .NET desktop development
	- Web & Cloud 
		+ ASP.NET and web development
		+ Azure development
		
	Se debe confirmar además de los "Individual components":
		+ .NET Framework 4.8 SDK
		+ .NET Framework 4.8 targeting pack
		
	En caso de contar con una instalación del Visual Studio 2019, proceder con la actualización a la última versión, 
	y confirmar que se tengan instalados los “workloads” e "Individual components" señalados en el punto anterior. 
	Esto se hace ejecutando el Visual Studio Installer, y aplicar en el equipo la actualización cuando aparece el 
	botón “Update”, es solo de aplicarlo y esperar que finalice.
 
	Se puede confirmar el resultado con el “Acerca de” de Visual Studio 2019. La versión a utilizar es la 16.9.4.
	
* Internet Information Services habilitado 
	http://technet.microsoft.com/en-us/library/cc731911.aspx
	
*	Web Deploy 3.6 o superior
	http://www.iis.net/downloads/microsoft/web-deploy.  El enlace del instalador se encuentra en la parte inferior 
	de la página.
	
* Navegadores Web actualizados a la última versión. 
- https://www.mozilla.org/en-US/firefox/
- https://www.google.com/chrome/index.html
- https://www.microsoft.com/en-us/edge

* Postman
	https://www.postman.com/downloads/

* Bases de datos de ejemplo
  https://github.com/Microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs
