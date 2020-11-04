CREATE TABLE [dbo].[cliente] (
    [idCliente]     INT          IDENTITY (1, 1) NOT NULL,
    [nombre]        VARCHAR (50) NULL,
    [apellido]      VARCHAR (50) NULL,
    [telefono]      VARCHAR (15) NULL,
    [direccion]     VARCHAR (15) NULL,
    [ciudad]        VARCHAR (50) NULL,
    [estado]        VARCHAR (50) NULL,
    [codigo_postal] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([idCliente] ASC)
);


CREATE TABLE [dbo].[libro] (
    [idLibro]        INT          IDENTITY (1, 1) NOT NULL,
    [titulo]         VARCHAR (50) NULL,
    [autor_nombre]   VARCHAR (50) NULL,
    [autor_apellido] VARCHAR (15) NULL,
    [categoria]      VARCHAR (50) NULL,
    [precio]         VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([idLibro] ASC)
);


CREATE TABLE [dbo].[orden] (
    [idOrden]     INT          IDENTITY (1, 1) NOT NULL,
    [idCliente]   INT          NULL,
    [idLibro]     INT          NULL,
    [fecha_orden] VARCHAR (50) NULL,
    PRIMARY KEY CLUSTERED ([idOrden] ASC),
    CONSTRAINT [Fk_orden_libro] FOREIGN KEY ([idCliente]) REFERENCES [dbo].[cliente] ([idCliente]),
    CONSTRAINT [Fk_orden_cliente] FOREIGN KEY ([idLibro]) REFERENCES [dbo].[libro] ([idLibro])
);



insert into cliente(nombre, apellido, telefono, ciudad, estado, codigo_postal) 
			values ('Juan', 'Martinez','04254454', 'Quito', 'Estado de Quito', '0881188');
insert into cliente(nombre, apellido, telefono, ciudad, estado, codigo_postal) 
			values ('Maria', 'Zamara','22342343', 'Cuenca', 'Estado de Cuenca', '3232');
			
insert into libro(titulo, autor_nombre, autor_apellido, categoria, precio)
			values ('Matematicas', 'Gauu','Jordan', 'Matematicas', '32.2');
insert into libro(titulo, autor_nombre, autor_apellido, categoria, precio)
			values ('Lenguaje', 'Cistobak','Colon', 'Idioma', '399.2');
			
insert into orden(idCliente, idLibro, fecha_orden)
			values ('1', '1','25 de Nov');

insert into orden(idCliente, idLibro, fecha_orden)
			values ('1', '2','30 de Nov');
			
insert into orden(idCliente, idLibro, fecha_orden)
			values ('2', '2','30 de Oct');
			