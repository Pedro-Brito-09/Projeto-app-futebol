CREATE TABLE IF NOT EXISTS jogadores (
    id INT AUTO_INCREMENT PRIMARY KEY,
    nome VARCHAR(100),
    idade INT,
    posicao VARCHAR(20)
);

CREATE TABLE IF NOT EXISTS jogos (
    id INT AUTO_INCREMENT PRIMARY KEY,
    data VARCHAR(20),
    local VARCHAR(100),
    campo VARCHAR(50),
    jogadores INT,
    maxTimes INT
);
