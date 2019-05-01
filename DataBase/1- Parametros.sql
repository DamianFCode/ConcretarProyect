SET IDENTITY_INSERT [dbo].[Parametro] ON 
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (4, N'General', N'BaseUrlBackend', N'Url backend', N'http://localhost:51249/')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (1, N'General', N'AppTitle', N'Nombre de la app', N'Sistema Ventas')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (9, N'Mails', N'Usuario', N'Indica el usuario de la cuenta', N'emailparatest.system@gmail.com')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (10, N'Mails', N'Contraseña', N'Indica contraseña de la cuenta', N'123456789+a')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (6, N'Mails', N'Servidor', N'Servidor de donde saldrán los mails', N'smtp.gmail.com')
GO
INSERT [dbo].[Parametro] ([ParametroId], [Categoria], [Clave], [Descripcion], [Valor]) VALUES (7, N'Mails', N'Puerto', N'Indica el puerto del servidor de correo', N'25')

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
SET IDENTITY_INSERT [dbo].[Parametro] OFF