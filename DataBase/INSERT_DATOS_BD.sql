
SET IDENTITY_INSERT [dbo].[Rol] ON 

GO
INSERT [dbo].[Rol] ([RolId], [Codigo], [Nombre]) VALUES (1, N'ADM', N'Administrador')
GO
SET IDENTITY_INSERT [dbo].[Rol] OFF
GO
SET IDENTITY_INSERT [dbo].[Usuario] ON 

GO
INSERT [dbo].[Usuario] ([UsuarioId], [Apellido], [Contrasena], [ContrasenaActualizada], [Email], [Habilitado], [Nombre], [TSCreado], [TSEliminado], [TSModificado]) VALUES (8, N'ADMIN', N'3oGnx94c4mF4CjH+qfFMBg==', 1, N'Pablopilato@gmail.con', 1, N'Admin', CAST(0x0700000000009B3F0B AS DateTime2), NULL, CAST(0x07F31F97A0BFA13F0B AS DateTime2))
GO
SET IDENTITY_INSERT [dbo].[Usuario] OFF
GO
INSERT [dbo].[UsuarioRol] ([RolId], [UsuarioId]) VALUES (1, 8)
GO
SET IDENTITY_INSERT [dbo].[Vista] ON 

GO
INSERT [dbo].[Vista] ([VistaId], [Descripcion], [Nombre]) VALUES (1, N'Perfil', N'Gestión de perfiles')
GO
INSERT [dbo].[Vista] ([VistaId], [Descripcion], [Nombre]) VALUES (2, N'Usuario', N'Gestión de usuarios')
GO
INSERT [dbo].[Vista] ([VistaId], [Descripcion], [Nombre]) VALUES (3, N'Parametro', N'Gestión de parámetros')
GO
SET IDENTITY_INSERT [dbo].[Vista] OFF
GO
SET IDENTITY_INSERT [dbo].[Permiso] ON 

GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (1, N'Listado de usuarios', N'Index', 2)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (2, N'Crear usuario', N'Create', 2)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (3, N'Editar usuario', N'Edit', 2)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (4, N'Eliminar usuario', N'Delete', 2)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (5, N'Listado de perfiles', N'Index', 1)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (6, N'Crear perfil', N'Create', 1)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (7, N'Editar perfil', N'Edit', 1)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (8, N'Eliminar perfil', N'Delete', 1)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (9, N'Ver parámetros', N'Index', 3)
GO
INSERT [dbo].[Permiso] ([PermisoId], [Descripcion], [Funcionalidad], [VistaId]) VALUES (10, N'Editar parámetro', N'Edit', 3)
GO
SET IDENTITY_INSERT [dbo].[Permiso] OFF
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 1)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 2)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 3)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 4)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 5)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 6)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 7)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 8)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 9)
GO
INSERT [dbo].[RolPermiso] ([RolId], [PermisoId]) VALUES (1, 10)
GO
INSERT [dbo].[__EFMigrationsHistory] ([MigrationId], [ProductVersion]) VALUES (N'20190501025454_EstructureBD', N'2.0.1-rtm-125')
GO
SET IDENTITY_INSERT [dbo].[Parametro] ON 

GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (1, N'General', N'AppTitle', N'Nombre de la app', N'Sistema Ventas')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (4, N'General', N'BaseUrlBackend', N'Url backend', N'http://localhost:51249/')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (6, N'Mails', N'Servidor', N'Servidor de donde saldrán los mails', N'smtp.gmail.com')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (7, N'Mails', N'Puerto', N'Indica el puerto del servidor de correo', N'25')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (9, N'Mails', N'Usuario', N'Indica el usuario de la cuenta', N'emailparatest.system@gmail.com')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (10, N'Mails', N'Contraseña', N'Indica contraseña de la cuenta', N'123456789+a')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (11, N'Mails', N'RemitenteNombre', N'Indica el from(quien envía el mail)', N'test@test.com')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (12, N'Path', N'PathMailing', N'Indica la ruta de la plantilla .html del mail', N'C:\Users\fabricio.gatica\Documents\NetCore\Projecto\ConcretarProyect\Concretar.BackEnd\wwwroot\mailing')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (28, N'Mails', N'MetodoEnvio', N'Forma de envio', N'Mailgun')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (29, N'Mails', N'Remitente', N'Indica el remitente ', N'test@test.com')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (30, N'Mail', N'EnableSsl', N'Indica si se va a usar enable', N'1')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (1001, N'Mail', N'MailSoporte', N'Mail soporte de System', N'system@system.com')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (1002, N'Key', N'MailGunApiKey', N'Key para la api de mailgun', N'a2286cabbc9925fb744a1e8ddfe44a00-52cbfb43-be443208')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (1003, N'Dominio', N'MailGunDominio', N'Dominio para la api de mailgun', N'sandbox0b005a61b6fa4f85a2e803de056b6886.mailgun.org')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (1004, N'Path', N'UrlApiMailgun', N'Url api de mailgun', N'https://api.mailgun.net/v3')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (1005, N'Key', N'3DESKey', N'Clave para criptar y descriptar password', N'System_Passkey')
GO
SET IDENTITY_INSERT [dbo].[Parametro] OFF
GO
SET IDENTITY_INSERT [dbo].[UsuarioToken] ON 

GO
INSERT [dbo].[UsuarioToken] ([UsuarioTokenId], [FechaExpiracion], [Token], [Usado], [UsuarioId]) VALUES (1, CAST(0x077FA0D9AE9AF83E0B AS DateTime2), N'7d3d9ea2-18e7-42f5-b0fc-f6fccfb169fb', 1, 8)
GO
SET IDENTITY_INSERT [dbo].[UsuarioToken] OFF
GO
