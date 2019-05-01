Declare @vistaId int
	
	INSERT [dbo].[Vista] ([Descripcion], [Nombre]) VALUES (N'Perfil', N'Gestión de perfiles')
	INSERT [dbo].[Vista] ([Descripcion], [Nombre]) VALUES (N'Usuario', N'Gestión de usuarios')
	INSERT [dbo].[Vista] ([Descripcion], [Nombre]) VALUES (N'Parametro', N'Gestión de parámetros')
    SET @vistaId = SCOPE_IDENTITY()

	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Listado de usuarios', N'Index', 2)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Crear usuario', N'Create', 2)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Editar usuario', N'Edit', 2)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Eliminar usuario', N'Delete', 2)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Listado de perfiles', N'Index', 1)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Crear perfil', N'Create', 1)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Editar perfil', N'Edit', 1)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Eliminar perfil', N'Delete', 1)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Ver parámetros', N'Index', 3)
	INSERT [dbo].[Permiso] ([Descripcion], [Funcionalidad], [VistaId]) VALUES (N'Editar parámetro', N'Edit', 3)
Go
INSERT [dbo].[Rol] ([Codigo], [Nombre]) VALUES (N'ADM', N'Administrador')

INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 1)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 2)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 3)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 4)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 5)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 6)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 7)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 8)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 9)
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 10)

INSERT [dbo].[UsuarioToken] ([FechaExpiracion], [Token], [Usado], [UsuarioId]) VALUES (CAST(0x077FA0D9AE9AF83E0B AS DateTime2), N'7d3d9ea2-18e7-42f5-b0fc-f6fccfb169fb', 1,1)