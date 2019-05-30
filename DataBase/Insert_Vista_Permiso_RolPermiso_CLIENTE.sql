Declare @VistaId int
Declare @PermisoId int
--INSERT VISTA
	INSERT INTO [dbo].[Vista]
           ([Descripcion]
           ,[Nombre])
     VALUES
           ('Cliente'
           ,'Gestion de clientes')
	
	SET @VistaId = SCOPE_IDENTITY()

--INSERT PERMISO INDEX
	INSERT INTO [dbo].[Permiso]
           ([Descripcion]
           ,[Funcionalidad]
           ,[VistaId])
     VALUES
           ('Listado de clientes'
           ,'Index'
           ,@VistaId)

	SET @PermisoId = SCOPE_IDENTITY()

--INSERT PERMISO AL ROL
	INSERT INTO [dbo].[RolPermiso]
           ([RolId]
           ,[PermisoId])
     VALUES
           (1
           ,@PermisoId)

--INSERT PERMISO CREATE
	INSERT INTO [dbo].[Permiso]
           ([Descripcion]
           ,[Funcionalidad]
           ,[VistaId])
     VALUES
           ('Crear cliente'
           ,'Create'
           ,@VistaId)

	SET @PermisoId = SCOPE_IDENTITY()

--INSERT PERMISO AL ROL
	INSERT INTO [dbo].[RolPermiso]
           ([RolId]
           ,[PermisoId])
     VALUES
           (1
           ,@PermisoId)

--INSERT PERMISO EDIT
	INSERT INTO [dbo].[Permiso]
           ([Descripcion]
           ,[Funcionalidad]
           ,[VistaId])
     VALUES
           ('Editar cliente'
           ,'Edit'
           ,@VistaId)

	SET @PermisoId = SCOPE_IDENTITY()

--INSERT PERMISO AL ROL
	INSERT INTO [dbo].[RolPermiso]
           ([RolId]
           ,[PermisoId])
     VALUES
           (1
           ,@PermisoId)

--INSERT PERMISO EDIT
	INSERT INTO [dbo].[Permiso]
           ([Descripcion]
           ,[Funcionalidad]
           ,[VistaId])
     VALUES
           ('Eliminar cliente'
           ,'Delete'
           ,@VistaId)

	SET @PermisoId = SCOPE_IDENTITY()

--INSERT PERMISO AL ROL
	INSERT INTO [dbo].[RolPermiso]
           ([RolId]
           ,[PermisoId])
     VALUES
           (1
           ,@PermisoId)
GO