USE [crud-cliente]

SELECT * FROM Clientes;
SELECT * FROM Enderecos;

-- Seleção dos clientes e seus respectivos Endereços
SELECT c.Nome as NOME_CLIENTE, e.Cep as CEP_ENDERECO
FROM Clientes c
INNER JOIN Enderecos e ON c.Id = e.ClienteId;

-- Seleção dos Endereços Fiscais
SELECT * FROM Enderecos e
WHERE e.Discriminator = 'EnderecoFiscal';

-- Seleção dos Endereços de Cobrança
SELECT * FROM Enderecos e
WHERE e.Discriminator = 'EnderecoCobranca';

-- Seleção dos Endereços de Entrega
SELECT * FROM Enderecos e
WHERE e.Discriminator = 'EnderecoEntrega';