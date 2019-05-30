Declare @VistaId int
Declare @PermisoId int
--INSERT VISTA
	INSERT INTO [dbo].[Vista]
           ([Descripcion]
           ,[Nombre])
     VALUES
           ('Reunion'
           ,'Gestion de Reuniones')
	
	SET @VistaId = SCOPE_IDENTITY()

--INSERT PERMISO INDEX
	INSERT INTO [dbo].[Permiso]
           ([Descripcion]
           ,[Funcionalidad]
           ,[VistaId])
     VALUES
           ('Listado de Reuniones'
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
           ('Crear Reunion'
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
           ('Editar Reunion'
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
           ('Eliminar reunion'
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