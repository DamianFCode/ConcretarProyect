Declare @VistaId int
Declare @PermisoId int
--INSERT VISTA
	INSERT INTO [dbo].[Vista]
           ([Descripcion]
           ,[Nombre])
     VALUES
           ('Proyecto'
           ,'Gestion de proyectos')
	
	SET @VistaId = SCOPE_IDENTITY()

--INSERT PERMISO INDEX
	INSERT INTO [dbo].[Permiso]
           ([Descripcion]
           ,[Funcionalidad]
           ,[VistaId])
     VALUES
           ('Listado de proyectos'
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
           ('Crear proyecto'
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
           ('Editar proyectos'
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
           ('Eliminar proyecto'
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