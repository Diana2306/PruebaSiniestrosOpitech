-- Script para poblar las tablas Departamento, Ciudad y TipoSiniestro
-- Base de datos: SiniestrosDb
-- SQL Server
-- Usa códigos DIVIPOLA del DANE directamente (CHAR(2) para departamentos, CHAR(5) para ciudades)

USE SiniestrosDb;
GO

-- ============================================
-- 1. INSERTAR DEPARTAMENTOS DE COLOMBIA (Códigos DIVIPOLA)
-- ============================================
-- Códigos DIVIPOLA: 2 dígitos (CHAR(2))

INSERT INTO Departamento (Id, Nombre) VALUES
('05', 'Antioquia'),
('08', 'Atlántico'),
('11', 'Bogotá D.C.'),
('13', 'Bolívar'),
('15', 'Boyacá'),
('17', 'Caldas'),
('18', 'Caquetá'),
('19', 'Cauca'),
('20', 'Cesar'),
('23', 'Córdoba'),
('25', 'Cundinamarca'),
('27', 'Chocó'),
('41', 'Huila'),
('44', 'La Guajira'),
('47', 'Magdalena'),
('50', 'Meta'),
('52', 'Nariño'),
('54', 'Norte de Santander'),
('63', 'Quindío'),
('66', 'Risaralda'),
('68', 'Santander'),
('70', 'Sucre'),
('73', 'Tolima'),
('76', 'Valle del Cauca'),
('81', 'Arauca'),
('85', 'Casanare'),
('86', 'Putumayo'),
('88', 'San Andrés y Providencia'),
('91', 'Amazonas'),
('94', 'Guainía'),
('95', 'Guaviare'),
('97', 'Vaupés'),
('99', 'Vichada');
GO

-- ============================================
-- 2. INSERTAR CIUDADES DE COLOMBIA (Códigos DIVIPOLA)
-- ============================================
-- Códigos DIVIPOLA: 5 dígitos (CHAR(5))

-- ANTIOQUIA (05)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('05001', '05', 'Medellín'),
('05088', '05', 'Bello'),
('05360', '05', 'Itagüí'),
('05266', '05', 'Envigado'),
('05615', '05', 'Rionegro'),
('05034', '05', 'Apartadó'),
('05885', '05', 'Turbo'),
('05897', '05', 'Yarumal'),
('05669', '05', 'Santa Rosa de Osos'),
('05576', '05', 'Puerto Berrío');

-- ATLÁNTICO (08)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('08001', '08', 'Barranquilla'),
('08758', '08', 'Soledad'),
('08470', '08', 'Malambo'),
('08638', '08', 'Sabanalarga'),
('08573', '08', 'Puerto Colombia');

-- BOGOTÁ D.C. (11)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('11001', '11', 'Bogotá');

-- BOLÍVAR (13)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('13001', '13', 'Cartagena'),
('13468', '13', 'Magangué'),
('13836', '13', 'Turbaco'),
('13052', '13', 'Arjona'),
('13248', '13', 'El Carmen de Bolívar');

-- BOYACÁ (15)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('15001', '15', 'Tunja'),
('15238', '15', 'Duitama'),
('15759', '15', 'Sogamoso'),
('15176', '15', 'Chiquinquirá'),
('15407', '15', 'Villa de Leyva');

-- CALDAS (17)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('17001', '17', 'Manizales'),
('17380', '17', 'La Dorada'),
('17616', '17', 'Riosucio'),
('17042', '17', 'Anserma'),
('17541', '17', 'Pensilvania');

-- CAQUETÁ (18)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('18001', '18', 'Florencia'),
('18753', '18', 'San Vicente del Caguán'),
('18247', '18', 'El Doncello'),
('18592', '18', 'Puerto Rico');

-- CASANARE (85)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('85001', '85', 'Yopal'),
('85010', '85', 'Aguazul'),
('87710', '85', 'Tauramena'),
('87473', '85', 'Villanueva');

-- CAUCA (19)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('19001', '19', 'Popayán'),
('19698', '19', 'Santander de Quilichao'),
('19573', '19', 'Puerto Tejada'),
('19517', '19', 'Patía');

-- CESAR (20)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('20001', '20', 'Valledupar'),
('20011', '20', 'Aguachica'),
('20175', '20', 'Codazzi'),
('20400', '20', 'La Paz');

-- CÓRDOBA (23)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('23001', '23', 'Montería'),
('23162', '23', 'Cereté'),
('23660', '23', 'Sahagún'),
('23417', '23', 'Lorica'),
('23555', '23', 'Planeta Rica');

-- CUNDINAMARCA (25)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('25754', '25', 'Soacha'),
('25269', '25', 'Facatativá'),
('25175', '25', 'Chía'),
('25307', '25', 'Girardot'),
('25899', '25', 'Zipaquirá'),
('25290', '25', 'Fusagasugá'),
('25430', '25', 'Madrid'),
('25473', '25', 'Mosquera'),
('25736', '25', 'Sibaté');

-- CHOCÓ (27)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('27001', '27', 'Quibdó'),
('27361', '27', 'Istmina'),
('27205', '27', 'Condoto'),
('27615', '27', 'Riosucio');

-- HUILA (41)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('41001', '41', 'Neiva'),
('41548', '41', 'Pitalito'),
('41298', '41', 'Garzón'),
('41396', '41', 'La Plata');

-- LA GUAJIRA (44)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('44001', '44', 'Riohacha'),
('44430', '44', 'Maicao'),
('44847', '44', 'Uribia'),
('44650', '44', 'San Juan del Cesar');

-- MAGDALENA (47)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('47001', '47', 'Santa Marta'),
('47189', '47', 'Ciénaga'),
('47288', '47', 'Fundación'),
('47053', '47', 'Aracataca');

-- META (50)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('50001', '50', 'Villavicencio'),
('50006', '50', 'Acacías'),
('50318', '50', 'Granada'),
('50689', '50', 'San Martín');

-- NARIÑO (52)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('52001', '52', 'Pasto'),
('52835', '52', 'Tumaco'),
('52356', '52', 'Ipiales'),
('52838', '52', 'Túquerres');

-- NORTE DE SANTANDER (54)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('54001', '54', 'Cúcuta'),
('54498', '54', 'Ocaña'),
('54520', '54', 'Pamplona'),
('54874', '54', 'Villa del Rosario');

-- PUTUMAYO (86)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('86001', '86', 'Mocoa'),
('86568', '86', 'Puerto Asís'),
('86885', '86', 'Villagarzón');

-- QUINDÍO (63)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('63001', '63', 'Armenia'),
('63130', '63', 'Calarcá'),
('63302', '63', 'La Tebaida'),
('63470', '63', 'Montenegro');

-- RISARALDA (66)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('66001', '66', 'Pereira'),
('66170', '66', 'Dosquebradas'),
('66682', '66', 'Santa Rosa de Cabal'),
('66400', '66', 'La Virginia');

-- SAN ANDRÉS Y PROVIDENCIA (88)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('88001', '88', 'San Andrés'),
('88564', '88', 'Providencia');

-- SANTANDER (68)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('68001', '68', 'Bucaramanga'),
('68276', '68', 'Floridablanca'),
('68307', '68', 'Girón'),
('68081', '68', 'Barrancabermeja'),
('68669', '68', 'San Gil');

-- SUCRE (70)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('70001', '70', 'Sincelejo'),
('70215', '70', 'Corozal'),
('70670', '70', 'Sampués'),
('70713', '70', 'San Onofre');

-- TOLIMA (73)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('73001', '73', 'Ibagué'),
('73268', '73', 'Espinal'),
('73449', '73', 'Melgar');

-- VALLE DEL CAUCA (76)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('76001', '76', 'Cali'),
('76520', '76', 'Palmira'),
('76109', '76', 'Buenaventura'),
('76834', '76', 'Tuluá'),
('76147', '76', 'Cartago'),
('76111', '76', 'Buga'),
('76895', '76', 'Yumbo'),
('76364', '76', 'Jamundí');

-- ARAUCA (81)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('81001', '81', 'Arauca');

-- AMAZONAS (91)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('91001', '91', 'Leticia');

-- GUAINÍA (94)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('94001', '94', 'Inírida');

-- GUAVIARE (95)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('95001', '95', 'San José del Guaviare');

-- VAUPÉS (97)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('97001', '97', 'Mitú');

-- VICHADA (99)
INSERT INTO Ciudad (Id, IdDepartamento, Nombre) VALUES
('99001', '99', 'Puerto Carreño');
GO

-- ============================================
-- 3. INSERTAR TIPOS DE SINIESTROS VIALES
-- ============================================

SET IDENTITY_INSERT dbo.TipoSiniestro ON;

INSERT INTO TipoSiniestro (Id, Nombre) VALUES
(1, 'Choque frontal'),
(2, 'Choque lateral'),
(3, 'Choque trasero'),
(4, 'Volcamiento'),
(5, 'Atropello'),
(6, 'Caída de ocupante'),
(7, 'Colisión con objeto fijo'),
(8, 'Incendio'),
(9, 'Salida de vía'),
(10, 'Colisión múltiple'),
(11, 'Choque con animal'),
(12, 'Otro');
GO

SET IDENTITY_INSERT dbo.TipoSiniestro OFF;

-- ============================================
-- VERIFICACIÓN
-- ============================================

SELECT 'Departamentos insertados: ' + CAST(COUNT(*) AS VARCHAR) FROM Departamento;
SELECT 'Ciudades insertadas: ' + CAST(COUNT(*) AS VARCHAR) FROM Ciudad;
SELECT 'Tipos de siniestros insertados: ' + CAST(COUNT(*) AS VARCHAR) FROM TipoSiniestro;
GO
