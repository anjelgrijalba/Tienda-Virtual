
SELECT f.Numero, f.Fecha, f.UsuariosId, l.Cantidad, p.Id, p.Nombre, p.Precio, u.Nick 
	FROM facturas f INNER JOIN lineasfactura l ON f.Id = l.FacturaId 
					INNER JOIN productos p ON p.Id = l.ProductoId
					INNER JOIN usuarios u ON u.Id=f.UsuariosId
					
	