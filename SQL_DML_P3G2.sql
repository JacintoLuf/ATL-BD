insert into agenda.logins (username, pass, tipo)
    values ('func','C340FF19831F32385E62D2F8F45068061F7362F4400F2583A64249E3333BB798','normal')
insert into agenda.logins (username, pass, tipo)
    values ('admin','AB25AF705EEDA0AAD6D61214FF69FB0A4B99FD32D86C30007BDBDD3F9BF7D5EE','mandachuva')

insert into agenda.grau values(1)
insert into agenda.grau values(2)
insert into agenda.grau values(3)
insert into agenda.grau values(4)

insert into agenda.ano values('2019/2020')


insert into agenda.professor (nome, phone, email)
	values('Calisto Costa', 915555539, 'c.costa@gmail.com')
insert into agenda.professor (nome, phone, email)
	values('Beatriz Silva', 915334530, 'bssilva@hotmail.com')
insert into agenda.professor (nome, phone, email)
	values('Maria de Lurdes', 935555852, 'lurdes.m@gmail.com')
insert into agenda.professor (nome, phone, email)
	values('Albano Rojão', 935551230, 'rojao123@gmail.com')
insert into agenda.professor (nome, phone, email)
	values('Jacinta Afonso', 965553050, 'jaf@hotmail.com')
insert into agenda.professor (nome, phone, email)
	values('Ana Paula Moreira', 935552214, 'paula71@hotmail.com')
insert into agenda.professor (nome, phone, email)
	values('Lúcia Matos', 922555301, 'lucia35m@outlook.com')


insert into agenda.atividade values('natação')
insert into agenda.atividade values('xadrez')
insert into agenda.atividade values('dança')


insert into agenda.orientado values('natação', 7)
insert into agenda.orientado values('xadrez', 4)
insert into agenda.orientado values('dança', 6)


insert into agenda.turma (grau, ano, id_prof) values(1, '2019/2020', 5)
insert into agenda.turma (grau, ano, id_prof) values(2, '2019/2020', 3)
insert into agenda.turma (grau, ano, id_prof) values(3, '2019/2020', 2)
insert into agenda.turma (grau, ano, id_prof) values(4, '2019/2020', 1)

insert into agenda.visita values('viagem a castelo de paiva', '2020-06-02', '09:00:00', '18:00:00')
insert into agenda.visita values('passeio de moliceiro', '2020-06-03', '15:00:00', '16:30:00')
insert into agenda.visita values('passeio pela cidade da Guarda', '2020-06-04', '09:00:00', '18:00:00')


--4 ano 2010
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao)
	values ('João Santos', 'Rua Assunção Guimarota nº5', '2010-06-12', 'asma', 'inalador', 'gluten')
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao)
	values ('Maria João Henriques', 'Rua do Castanhal Pousos', '2010-06-12', 'hipotensão', 'cipro', 'lactose')
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao)
	values ('Mariana Silva', 'Rua Virgílio Monteiro nº2', '2010-06-12', 'asma', 'inalador', 'açucar')
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao)
	values ('Pedro Henriques', 'Rua Comissão de Iniciativa nº3 1ºdt', '2010-06-12', 'hipertensão', 'clorotiazida', 'sal, lactose')
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao)
	values ('Martim Jesus', 'Rua do Soutelo nº122', '2010-06-12', 'diabetes', 'insulina', 'lactose, açucar')
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao, antipiretico)
	values ('André Luis Pais', 'Rua do Soutelo nº110', '2010-06-12', 'asma', 'inalador', 'gluten', 'paracetamol')
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao, antipiretico)
	values ('Maria Fidalgo', 'Largo da Capela nº1 5ºf', '2010-02-28', 'asma', 'inalador', 'lactose', 'brufen')
insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao, antipiretico)
	values ('Ruben Carneiro', 'Rua Principal Antuzede nº4', '2010-02-12', 'asma', 'inalador', 'lactose', 'brufen')
insert into agenda.aluno (nome, morada, nasc)
	values ('João Cavadas', 'Avenida da Liberdade nº200 4ºf', '2010-3-31')
insert into agenda.aluno (nome, morada, nasc)
	values ('Luisa Ferreira', 'Avenido dos Bois nº5', '2010-4-30')
insert into agenda.aluno (nome, morada, nasc)
	values ('Ana Sofia Andrade', 'Estrada St. Antonio', '2010-01-05')

insert into agenda.pertence_turma
select agenda.aluno.id, agenda.turma.id from agenda.aluno, agenda.turma where agenda.aluno.id<=11 and agenda.turma.grau = 4;

insert into agenda.tem
select agenda.aluno.id, agenda.atividade.nome from agenda.aluno, agenda.atividade where (agenda.aluno.id between 2 and 6) and agenda.atividade.nome = 'natação';
insert into agenda.tem
select agenda.aluno.id, agenda.atividade.nome from agenda.aluno, agenda.atividade where (agenda.aluno.id between 8 and 10) and agenda.atividade.nome = 'dança';

insert into agenda.vai
select agenda.aluno.id, agenda.visita.data from agenda.aluno, agenda.visita where agenda.aluno.id<=11 and agenda.visita.data = '2020-06-2';



--3 ano 2011
insert into agenda.aluno (nome, morada, nasc)
	values ('Filipa Soares', 'Entroncamento nº3, porto', '2011-11-23')
insert into agenda.aluno (nome, morada, nasc)
	values ('Diogo Meireles', 'Estrada dos Cavalinhos nº1', '2011-7-10')
insert into agenda.aluno (nome, morada, nasc)
	values ('Carlos João Costa', 'Rua dos Palácios nº155', '2011-01-20')
insert into agenda.aluno (nome, morada, nasc)
	values ('Manuel Afonso Sobral', 'Olho Dagua Bloco C 1ºd', '2011-5-15')
insert into agenda.aluno (nome, morada, nasc)
	values ('Victoria dos Santos Pereira', 'Estrada St. Antonio', '2011-10-12')

insert into agenda.pertence_turma
select agenda.aluno.id, agenda.turma.id from agenda.aluno, agenda.turma where agenda.aluno.id>11 and agenda.turma.grau = 3;

insert into agenda.tem
select agenda.aluno.id, agenda.atividade.nome from agenda.aluno, agenda.atividade where agenda.aluno.id>11 and agenda.atividade.nome = 'xadrez';

insert into agenda.vai
select agenda.aluno.id, agenda.visita.data from agenda.aluno, agenda.visita where agenda.aluno.id>11 and agenda.visita.data = '2020-06-2';


--2 ano 2012
insert into agenda.aluno (nome, morada, nasc)
	values ('Joana Leonor Ferreira', 'Estrada St. Julião', '2012-07-01')
insert into agenda.aluno (nome, morada, nasc, problemas, restricao, antipiretico)
	values ('Clara Silveira', 'Rua Macau Bairro Fonte da Moura', '2012-04-15', 'diabetes', 'açucar, lactose', 'brufen')
insert into agenda.aluno (nome, morada, nasc, restricao, antipiretico)
	values ('Ricardo Belém Rios', 'Rua Guiné Bairro Fonte da Moura nº10 1dt', '2012-06-01', 'sal, lactose', 'paracetamol')
insert into agenda.aluno (nome, morada, nasc)
	values ('Abel Martinho', 'Rua Maria do Carmo nº5 3ºesq', '2012-07-01')
insert into agenda.aluno (nome, morada, nasc)
	values ('Francisco Paixão', 'Rua D. Dinis nº8 3800-234, Aveiro', '2012-05-05')

insert into agenda.pertence_turma
select agenda.aluno.id, agenda.turma.id from agenda.aluno, agenda.turma where agenda.aluno.id>16 and agenda.turma.grau = 2;

insert into agenda.tem
select agenda.aluno.id, agenda.atividade.nome from agenda.aluno, agenda.atividade where agenda.aluno.id>16 and agenda.atividade.nome = 'natação';

insert into agenda.vai
select agenda.aluno.id, agenda.visita.data from agenda.aluno, agenda.visita where agenda.aluno.id>16 and agenda.visita.data = '2020-06-3';


--1 ano 2013
insert into agenda.aluno (nome, morada, nasc)
	values ('Maria Leonor Ferreira', 'Estrada St. João', '2012-07-01')
insert into agenda.aluno (nome, morada, nasc, problemas, restricao, antipiretico)
	values ('Clara Brás', 'Cova da Moura', '2012-04-15', 'diabetes', 'açucar, lactose', 'brufen')
insert into agenda.aluno (nome, morada, nasc, restricao, antipiretico)
	values ('Manuel Rios', 'Rua da Moura nº10 1dt', '2012-06-01', 'sal, lactose', 'paracetamol')
insert into agenda.aluno (nome, morada, nasc)
	values ('Gabriela Martins', 'Rua Cravo e Canela nº5 3ºesq', '2012-07-01')
insert into agenda.aluno (nome, morada, nasc)
	values ('Diogo Cão', 'Rua D. Sebastião nº8 2725-246, Aveiro', '2012-05-05')

insert into agenda.pertence_turma
select agenda.aluno.id, agenda.turma.id from agenda.aluno, agenda.turma where agenda.aluno.id>21 and agenda.turma.grau = 1;

insert into agenda.tem
select agenda.aluno.id, agenda.atividade.nome from agenda.aluno, agenda.atividade where agenda.aluno.id>22 and agenda.atividade.nome = 'dança';

insert into agenda.vai
select agenda.aluno.id, agenda.visita.data from agenda.aluno, agenda.visita where agenda.aluno.id>21 and agenda.visita.data = '2020-06-4';



------------------------------------------------------------------------------------------------------------------------------------------------------
--4 ano
insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Maria João Santos', 'Rua Assunção Guimarota nº5', 920555495, 'maryjane@outlook.com', 'Esteticista')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (1,920555495,'mãe')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (1,920555495)
insert into agenda.levantado (id_aluno, parent_phone)
	values (1,920555495)

insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Afonso Henriques', 'Rua do Castanhal Pousos', 965557014, 'afhenri@gmail.com', 'Contabilista')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (2,965557014,'pai')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (2,965557014)
insert into agenda.levantado (id_aluno, parent_phone)
	values (2,965557014)

insert into agenda.adulto (nome, morada, phone)
	values ('Osório Silva', 'Rua Virgílio Monteiro nº4', 929455557)
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (3,929455557,'tio')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (3,929455557)
insert into agenda.levantado (id_aluno, parent_phone)
	values (3,929455557)

insert into agenda.adulto (nome, morada, phone, wphone, email, ltrabalho, profissao)
	values ('João Henriques', 'Rua Comissão de Iniciativa nº3 1ºdt', 922555309, 922512309, 'jhenriques@gmail.com', 'BES porto', 'Bancario')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (4,922555309,'pai')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (4,965557014,'tio')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (4,922555309)
insert into agenda.levantado (id_aluno, parent_phone)
	values (4,922555309)
insert into agenda.levantado (id_aluno, parent_phone)
	values (4,965557014)
insert into agenda.levantado (id_aluno, parent_phone)
	values (2,922555309)

insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Joaquina Jesus', 'Rua do Soutelo nº122', 935556620, 'jjesus@hotmail.com', 'Advogada')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (5,935556620,'mãe')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (5,935556620)
insert into agenda.levantado (id_aluno, parent_phone)
	values (5,935556620)

insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Maria João Pais', 'Rua do Soutelo nº110', 922555692, 'maria.jp@gmail.com', 'Engenheira')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (6,922555692,'mãe')
insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('António Pais', 'Rua do Soutelo nº110', 922555048, 'antonio.p@gmail.com', 'Engenheiro')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (6,922555048,'pai')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (6,929455557,'padrinho')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (6,922555692)
insert into agenda.levantado (id_aluno, parent_phone)
	values (6,922555692)
insert into agenda.levantado (id_aluno, parent_phone)
	values (6,922555048)
insert into agenda.levantado (id_aluno, parent_phone)
	values (6,929455557)

insert into agenda.adulto (nome, morada, phone)
	values ('Carlos Fidalgo', 'Largo da Capela nº1 5ºf', 965553050)
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (7,965553050,'pai')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (7,965553050)
insert into agenda.levantado (id_aluno, parent_phone)
	values (7,965553050)

insert into agenda.adulto (nome, morada, phone)
	values ('Joana Carneiro', 'Rua Principal Antuzede nº4', 915551683)
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (8,915551683,'mãe')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (8,915551683)
insert into agenda.levantado (id_aluno, parent_phone)
	values (8,915551683)

insert into agenda.adulto (nome, morada, phone)
	values ('Ana Pureza Cavadas', 'Avenida da Liberdade nº200 4ºf', 921555073)
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (9,921555073,'mãe')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (9,921555073)
insert into agenda.levantado (id_aluno, parent_phone)
	values (9,921555073)

insert into agenda.adulto (nome, morada, phone)
	values ('Diogo Ferreira', 'Avenido dos Bois nº5', 915555648)
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (10,915555648,'irmão')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (10,915555648)
insert into agenda.levantado (id_aluno, parent_phone)
	values (10,915555648)

insert into agenda.adulto (nome, morada, phone)
	values ('Sonia Andrade', 'Estrada St. Antonio', 935554010)
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (11,935554010,'irmã')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (11,935554010)
insert into agenda.levantado (id_aluno, parent_phone)
	values (11,935554010)


--3 ano
insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('João Soares', 'Entroncamento nº3, porto', 931929240, 'jotasoares@gmail.com', 'Professor')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (12,931929240,'pai')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (12,931929240)
insert into agenda.levantado (id_aluno, parent_phone)
	values (12,931929240)

insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Joaquim Meireles', 'Estrada dos Cavalinhos nº1', 935559211, 'meireles.quim@gmail.com', 'Carpinteiro')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (13,935559211,'pai')
insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Diana Meireles', 'Estrada dos Cavalinhos nº1', 969185543, 'd.meireles@outlook.com', 'Carpinteira')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (13,969185543,'mae')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (13,935559211)
insert into agenda.levantado (id_aluno, parent_phone)
	values (13,935559211)
insert into agenda.levantado (id_aluno, parent_phone)
	values (13,969185543)

insert into agenda.adulto (nome, morada, phone)
	values ('João Costa', 'Rua dos Palácios nº155', 933209256)
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (14,933209256,'pai')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (14,933209256)
insert into agenda.levantado (id_aluno, parent_phone)
	values (14,933209256)

insert into agenda.adulto (nome, morada, phone, hphone, wphone, email, ltrabalho, profissao)
	values ('Juliana Sobral', 'Olho Dagua Bloco C 1ºd', 967485545, 234180552, 919457229, 'juls.sobral@hotmail.com', 'Pastelaria Bom Gosto Aveiro', 'Confeiteira')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (15,967485545,'mãe')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (15,967485545)
insert into agenda.levantado (id_aluno, parent_phone)
	values (15,967485545)

insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Amélia dos Santos Pereira', 'Estrada St. Antonio', 912399213, 'melisp@gmail.com', 'Professora')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (16,912399213,'mãe')
insert into agenda.adulto (nome, morada, phone, email, profissao)
	values ('Luis dos Santos Pereira', 'Estrada St. Antonio', 929455535, 'luigi.sp@gmail.com', 'Estudante')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
	values (16,929455535,'irmão')
insert into agenda.encarregado (id_aluno, parent_phone)
	values (16,912399213)
insert into agenda.levantado (id_aluno, parent_phone)
	values (16,912399213)
insert into agenda.levantado (id_aluno, parent_phone)
	values (16,929455535)


--2 ano
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Cristina Ferreira', 'Estrada St. Julião', 959741102, 'cf@gmail.com', 'Escriturária')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (17,959741102,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (17,959741102)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (17,959741102)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('António Gustavo Ferreira', 'Estrada St. Julião', 951929749, 'gustavoferreirinha@hotmail.com', 'Carpinteiro')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (17,951929749,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (17,951929749)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Natália Bis Silveira', 'Rua Macau Bairro Fonte da Moura', 951752164, 'bissilveira@sapo.pt', 'Encenadora')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (18,951752164,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (18,951752164)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (18,951752164)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Fernando Abutre Silveira', 'Rua Macau Bairro Fonte da Moura', 953571697, 'fasilveira@ua.pt', 'Professor')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (18,953571697,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (18,953571697)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Inês Belém Rios', 'Rua Guiné Bairro Fonte da Moura nº10 1dt', 957278292, 'iinestejo@gmail.pt', 'Enfermeira')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (19,957278292,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (19,957278292)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (19,957278292)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('João Belém Rios', 'Rua Guiné Bairro Fonte da Moura nº10 1dt', 959547934, 'jbr@ua.pt', 'Professor')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (19,959547934,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (19,959547934)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Micaela Martinho', 'Rua Maria do Carmo nº5 3ºesq', 950041784, 'mcm@ua.pt', 'Professora')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (20,950041784,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (20,950041784)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (20,950041784)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Caim Martinho', 'Rua Maria do Carmo nº5 3ºesq', 953220296, 'cmar@gmail.pt', 'Alfaiate')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (20,953220296,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (20,953220296)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Gabriela Paixão', 'Rua D. Dinis nº8 3800-234, Aveiro', 951484408, 'apaixonada@hotmail.com', 'Controladora de Tráfego Aéreo')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (21,951484408,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (21,951484408)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (21,951484408)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Gabriel Paixão', 'Rua D. Dinis nº8 3800-234, Aveiro', 956215667, 'apaixonado@gmail.pt', 'Desempregado')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (21,956215667,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (21,956215667)


--1 ano
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Juliana Ferreira', 'Estrada St. João', 929688093, 'julsf@gmail.com', 'Contabilista')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (22,929688093,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (22,929688093)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (22,929688093)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Gonçalo Ferreira', 'Estrada St. João', 929916724, 'goncaloferreira@ua.pt', 'Professor')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (22,929916724,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (22,929916724)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Ana Brás', 'Cova da Moura', 921881236, 'aninhas@gmail.com', 'Assistente Social')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (23,921881236,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (23,921881236)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Henrique Brás', 'Cova da Moura', 926898430, 'henbras@hotmail.com', 'Cabeleireiro')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (23,926898430,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (23,926898430)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (23,926898430)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Fernanda Rios', 'Rua da Moura nº10 1dt', 929920963, 'fernandadouro@gmail.com', 'Desempregada')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (24,929920963,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (24,929920963)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Ricardo Laranjinha Rios', 'Rua da Moura nº10 1dt', 925453703, 'laranjinha@hotmail.com', 'Contabilista')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (24,925453703,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (24,925453703)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (24,925453703)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Joaquina Martins', 'Rua Cravo e Canela nº5 3ºesq', 928101465, 'jmar@gmail.com', 'Empregada de Mesa')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (25,928101465,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (25,928101465)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Paulo Martins', 'Rua Cravo e Canela nº5 3ºesq', 925599803, 'paulao@hotmail.com', 'Coreógrafo')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (25,925599803,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (25,925599803)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (25,925599803)

insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Filipa Cão', 'Rua D. Sebastião nº8 2725-246, Aveiro', 920689318, 'ficao@gmail.com', 'Auditora')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (26,920689318,'mãe')
insert into agenda.levantado (id_aluno, parent_phone)
    values (26,920689318)
insert into agenda.adulto (nome, morada, phone, email, profissao)
    values ('Rui Cão', 'Rua D. Sebastião nº8 2725-246, Aveiro', 926323352, 'rucao@hotmail.com', 'Chapeleiro')
insert into agenda.parente (id_aluno, parent_phone, parentesco)
    values (26,926323352,'pai')
insert into agenda.levantado (id_aluno, parent_phone)
    values (26,926323352)
insert into agenda.encarregado (id_aluno, parent_phone)
    values (26,926323352)