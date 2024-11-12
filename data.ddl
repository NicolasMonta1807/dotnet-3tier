CREATE TABLE espacios (
	id INT PRIMARY KEY AUTO_INCREMENT,
    nombre VARCHAR(64) NOT NULL,
    hora_apertura TIME NOT NULL,
    hora_cierre TIME NOT NULL
);

CREATE TABLE horarios(
	id INT PRIMARY KEY AUTO_INCREMENT,
    espacio_id INT NOT NULL,
    hora_inicio TIME NOT NULL,
    hora_fin TIME NOT NULL,
    capacidad INT NOT NULL,
    FOREIGN KEY (espacio_id) REFERENCES espacios(id)
);

