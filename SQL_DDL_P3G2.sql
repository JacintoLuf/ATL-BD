 --------------------TABELAS----------------------
create schema agenda;
GO
create table agenda.grau (
	grau			int primary key NOT NULL check(Grau > 0)
);
create table agenda.ano (
	ano				varchar(9) primary key NOT NULL
);
create table agenda.professor (
	id				int identity(1,1) primary key NOT NULL,
	nome			varchar(70)	NOT NULL,
	phone			numeric(9) unique NOT NUll,
	email			varchar(50) unique NOT NULL,
	constraint check_emailP check(email like '%_@__%.__%')
);
create table agenda.atividade (
	nome			varchar(30) primary key NOT NULL
);
create table agenda.orientado (
	nome			varchar(30) NOT NULL,
	id_prof			int NOT NULL,
	foreign key(nome) references agenda.atividade(nome) on update cascade,
	foreign key(id_prof) references agenda.professor(id) on update cascade,
	primary key (nome, id_prof)
);
create table agenda.turma (
	id				int identity(1,1) primary key NOT NULL,
	grau			int NOT NULL,
	ano				varchar(9) NOT NULL,
	id_prof			int NOT NULL,
	foreign key(grau) references agenda.grau(grau) on update cascade,
	foreign key(ano) references agenda.ano(ano) on update cascade,
	foreign key(id_prof) references agenda.professor(id)
);
create table agenda.aluno (
	id				int identity(1,1) primary key,
	nome			varchar(70) NOT NULL,
	morada			varchar(100) NOT NULL,
	nasc			date NOT NULL,
	problemas		varchar(100),
	medicacao		varchar(100),
	restricao		varchar(100),
	antipiretico	varchar(100)
);
create table agenda.pertence_turma (
	id_aluno		int NOT NULL,
	id_turma		int NOT NULL,
	foreign key(id_aluno) references agenda.aluno(id) on update cascade,
	foreign key(id_turma) references agenda.turma(id) on update cascade,
	primary key (id_aluno, id_turma)
);
create table agenda.tem (
	id_aluno		int	NOT NULL,
	nome			varchar(30) NOT NULL,
	foreign key(id_aluno) references agenda.aluno(id) on update cascade,
	foreign key(nome) references agenda.atividade(nome) on update cascade,
	primary key(id_aluno,nome)
);
create table agenda.adulto (
	nome			varchar(70) NOT NULL,
	morada			varchar(100) NOT NULL,
	phone			numeric(9) primary key NOT NULL,
	hphone			numeric(9),
	wphone			numeric(9),
	email			varchar(50),
	ltrabalho		varchar(100),
	profissao		varchar(50),
	constraint check_emailA check(email like '%_@__%.__%' or email like null)
);
create table agenda.encarregado (
	id_aluno		int NOT NULL,
	parent_phone	numeric(9) NOT NULL,
	foreign key(id_aluno) references agenda.aluno(id) on update cascade,
	foreign key(parent_phone) references agenda.adulto(phone) on update cascade,
	primary key(id_aluno)
);
create table agenda.parente (
	id_aluno		int NOT NULL,
	parent_phone	numeric(9) NOT NULL,
	parentesco		varchar(10) NOT NULL,
	foreign key(id_aluno) references agenda.aluno(id) on update cascade,
	foreign key(parent_phone) references agenda.adulto(phone) on update cascade,
	primary key(id_aluno,parent_phone)
);
create table agenda.levantado (
	id_aluno		int NOT NULL,
	parent_phone	numeric(9) NOT NULL,
	foreign key(id_aluno) references agenda.aluno(id) on update cascade,
	foreign key(parent_phone) references agenda.adulto(phone) on update cascade,
	primary key(id_aluno,parent_phone)
);
create table agenda.visita (
	nome			varchar(100) NOT NUll,
	data			date primary key NOT NUll,
	partida			time NOT NUll,
	chegada			time NOT NUll
);
create table agenda.vai (
	id_aluno		int NOT NULL,
	visita			date NOT NULL,
	foreign key(id_aluno) references agenda.aluno(id) on update cascade,
	foreign key(visita) references agenda.visita(data) on update cascade,
	primary key(id_aluno,visita)
);

create table agenda.logins (
    username varchar(100) not null,
    pass varchar(128) not null,
    tipo varchar(64) not null,
    primary key(username)
);


 --------------------INDEXS---------------------

--index criado para facilitar a pesquisa das turmas
create nonclustered index turma_ano_grau on agenda.turma(ano)
include(grau);

create nonclustered index turma_ano on agenda.turma(ano);

--index para diminuir o tempo de pesquisa dos alunos por nome
create nonclustered index aluno_nome on agenda.aluno(nome);

--index para diminuir o tempo de pesquisa dos adultos por nome
create nonclustered index adulto_nome on agenda.adulto(nome);

--index para diminuir o tempo de pesquisa dos professores por nome
create nonclustered index prof_nome on agenda.professor(nome);

--index para ver as várias edições da visita
create nonclustered index visita_nome on agenda.visita(nome);

--index para pesquisar aluno através do nome covering data
create nonclustered index aluno_nome_data on agenda.aluno(nome)
include(nasc)