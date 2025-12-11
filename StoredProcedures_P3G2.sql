
----------------------------------------------------------Stored Procedures---------------------------------------------------------------
------------------------------autorizaçao------------------------------------
--sp para checkar login
create proc agenda.autorization (@user varchar(100), @pas varchar(64))
as

    select logins.tipo
    from agenda.logins
    where logins.username = @user and logins.pass = @pas
go
--exec agenda.autorization 'admin','AB25AF705EEDA0AAD6D61214FF69FB0A4B99FD32D86C30007BDBDD3F9BF7D5EE'

------------------------------returns ids------------------------------------
--retorna o id de um aluno
create proc agenda.student_id (@name varchar(70), @birthday date, @ide int OUTPUT)
as 
    select @ide = aluno.id from agenda.aluno with(index([aluno_nome_data])) 
    where agenda.aluno.nome = @name and agenda.aluno.nasc = @birthday
go
--declare @ide as int;
--exec agenda.student_id 'Mariana Silva', '2010-06-12', @ide OUTPUT;
--print @ide;

--sp que devolve id de turma pelo grau e o ano
create proc agenda.class_id (@degree int, @year varchar(9), @classid int OUTPUT)
as
    select @classid = turma.id from agenda.turma with(index([turma_ano_grau]))
    where agenda.turma.ano = @year and agenda.turma.grau = @degree
go
--declare @classid as int;
--exec agenda.class_id 1, '2019/2020', @classid OUTPUT;
--print @classid;

--sp para retornar id de um prof atraves do mail
create proc agenda.prof_id (@mail varchar(50), @profid int OUTPUT)
as
    select @profid = professor.id from agenda.professor
    where professor.email = @mail
go
--declare @profid as int;
--exec agenda.prof_id 'bssilva@hotmail.com',@profid OUTPUT;
--print @profid;

-------------------------------record-sets------------------------------------
--Lista de alunos de uma certa turma(ano letivo, grau)
create proc agenda.specific_class (@degree int, @year varchar(9))
as
    declare @cid as int;
    exec agenda.class_id @degree,@year,@cid OUTPUT;

    select aluno.nome, aluno.nasc from agenda.pertence_turma
    join agenda.aluno on aluno.id = pertence_turma.id_aluno
    where agenda.pertence_turma.id_turma = @cid
go
--exec agenda.specific_class 4,'2019/2020';
--list de anos letivos
create proc agenda.anos_letivos
as 

    select *
    from agenda.ano
go
--exec agenda.anos_letivos;
--lista de alunos
create proc agenda.todos_alunos
as
	select * from agenda.aluno
go
--exec agenda.todos_alunos;
--Lista de visitas de estudo
create proc agenda.visitas
as
	select *
	from agenda.visita
go
--exec agenda.visitas;
--Lista de atividade extracurricular e professor
create proc agenda.profs_atividade
as
	select atividade.nome as atividade, professor.nome as prof, email 
	from agenda.atividade left join agenda.orientado on atividade.nome=orientado.nome left join agenda.professor on professor.id=orientado.id_prof
go
--exec agenda.profs_atividade;
--lista de professores
create proc agenda.todos_profs
as
	select * from agenda.professor
go
--exec agenda.todos_profs
--Lista de turmas e professor
create proc agenda.prof_turmas
as
	select turma.id as class_id, ano, grau, professor.id as prof_id, professor.nome, professor.email, professor.phone
	from agenda.turma join agenda.professor on id_prof=professor.id
go
--exec agenda.prof_turmas;
--Atividades a que pertence certo aluno
create proc agenda.student_atividade @nome varchar(70), @nasc date
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	select atividade.nome from agenda.atividade join agenda.tem on atividade.nome=tem.nome
	where tem.id_aluno=@ide
go
--exec agenda.student_atividade 'Mariana Silva', '2010-06-12';
--Visitas a que pertence certo aluno
create proc agenda.student_visita @nome varchar(70), @nasc date
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	select visita.nome, data from agenda.visita join agenda.vai on data=vai.visita
	where vai.id_aluno=@ide
go
--exec agenda.student_visita 'Mariana Silva', '2010-06-12';
--informaçao de um aluno
create proc agenda.info_aluno @nome varchar(70), @nasc date
as
	select * from agenda.aluno
	where nome=@nome and nasc=@nasc
go
--exec agenda.info_aluno 'Mariana Silva', '2010-06-12';
--encarregado de educação de um certo aluno
create proc agenda.aluno_encarregado @nome varchar(70), @nasc date
as
	select adulto.nome as nome, phone
	from agenda.aluno join agenda.encarregado on aluno.id=id_aluno join agenda.adulto on phone=parent_phone
	where aluno.nome=@nome and nasc=@nasc
go
--exec agenda.aluno_encarregado 'Pedro Henriques','2010-06-12';
--informaçao de um adulto
create proc agenda.info_adulto @phone numeric(9)
as
	select * from agenda.adulto
	where phone=@phone
go
--exec agenda.info_adulto 935559211;
--informaçao do parentesco de um adulto em relaçao a um aluno
create proc agenda.info_parente @nome varchar(70), @nasc date, @phone numeric(9)
as
	declare @ide int;
	exec agenda.student_id @nome, @nasc, @ide output;

	select parentesco from agenda.parente
	where id_Aluno=@ide and parent_phone=@phone
go
--exec agenda.info_parente 'Mariana Silva', '2010-06-12', 929455557;
--sp para adultos relacionados a certo aluno
create proc agenda.student_related (@name varchar(70), @birthday date)
as
    declare @sid as int;
    exec agenda.student_id @name,@birthday,@sid OUTPUT;

    select aluno.nome as aluno, adulto.nome as adulto, phone from agenda.adulto
                                join agenda.levantado on phone=levantado.parent_phone
                                join agenda.aluno on id=id_aluno
    where aluno.id=@sid
go
--exec agenda.student_related 'Pedro Henriques','2010-06-12';
--informaçao de um adulto
create proc agenda.info_adulto @phone numeric(9)
as
	select * from agenda.adulto
	where phone=@phone
go
--exec agenda.info_adulto 935559211;
--informaçao do parentesco de um adulto em relaçao a um aluno
create proc agenda.info_parente @nome varchar(70), @nasc date, @phone numeric(9)
as
	declare @ide int;
	exec agenda.student_id @nome, @nasc, @ide output;

	select parentesco from agenda.parente
	where id_Aluno=@ide and parent_phone=@phone
go
--exec agenda.info_parente 'Mariana Silva', '2010-06-12', 929455557;

-------------------------------inserts----------------------------------------
--Insert vai(nome,datanasc,datavisita)
create proc agenda.insert_vai @nome varchar(70), @nasc date, @visita date
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	insert into agenda.vai values(@ide, @visita)

go
--exec agenda.insert_vai 'João Santos', '2010-06-12', '2020-06-02';
--Insert visita de estudo
create proc agenda.insert_visita @nome varchar(100), @data date, @partida time, @chegada time
as
	insert into agenda.visita values(@nome, @data, @partida, @chegada)

go
--exec agenda.insert_visita 'João Santos', '2020-06-03', '00:00:00', '00:00:00';
--Insert pertence atividade(nome, data, nomeatividade)
create proc agenda.insert_tem @nome varchar(70), @nasc date, @atividade varchar(30)
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	insert into agenda.tem values(@ide, @atividade)
go
--exec agenda.insert_tem 'Maria João Henriques', '2010-06-12', 'natação';
--Insert atividade
create proc agenda.insert_atividade @nome varchar(30)
as
	insert into agenda.atividade values(@nome)
	
go
--exec agenda.insert_atividade 'natação';
--Insert turma(tudo, mail do prof)
create proc agenda.insert_turma @grau int, @ano varchar(9), @prof_mail varchar(50)
as
	declare @ide int
	exec agenda.prof_id @prof_mail ,@ide OUTPUT
	if(select count(*) from agenda.turma where grau=@grau and ano=@ano) = 0
		begin
			insert into agenda.turma values(@grau, @ano, @ide)
		end
	else
		begin
			update agenda.turma
			set grau=@grau, ano=@ano, id_prof=@ide
			where grau=@grau and ano=@ano
		end
go
--exec agenda.insert_turma 1,'2019/2020','lurdes.m@gmail.com';
--insert orientado
create proc agenda.insert_orientado @nome varchar(30), @prof_mail varchar(50)
as
	declare @ide int
	exec agenda.prof_id @prof_mail ,@ide OUTPUT
	if(select count(*) from agenda.orientado where nome=@nome) = 0
		begin
			insert into agenda.orientado values(@nome,@ide)
		end
	else
		begin
			update agenda.orientado
			set nome=@nome, id_prof=@ide
			where nome=@nome
		end
go
--exec agenda.insert_orientado'futebol', 'c.costam@gmail.com';
--Insert pertence_turma(nome, data, anoletivo, grau)
create proc agenda.insert_pertence_turma @nome varchar(70), @nasc date, @ano varchar(9), @grau int
as
	declare @ide int
	declare @idt int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	exec agenda.class_id @grau, @ano, @idt OUTPUT;
	if(select count(*) from agenda.pertence_turma join agenda.turma on id=id_turma where id_aluno = @ide and ano = @ano) = 0
		begin
			insert into agenda.pertence_turma values(@ide, @idt)
		end
	else
		begin
			declare @ids int
			select @ids=id_turma from agenda.pertence_turma join agenda.turma on id=id_turma where id_aluno = @ide and ano = @ano
			update agenda.pertence_turma
			set id_aluno=@ide, id_turma=@idt
			where id_aluno=@ide and id_turma=@ids
		end
go
--exec agenda.insert_pertence_turma'Maria João Henriques', '2010-06-12', '2019/2020', 4;
--Insert professor
create proc agenda.insert_professor
@nome varchar(70),
@phone numeric(9),
@email varchar(50)
as
	insert into agenda.professor values(@nome, @phone, @email)

go
--exec agenda.insert_professor 'Calisto Costa', 915555539, 'c.costa@gmail.com';
--atualizar prof
create proc agenda.update_professor
@nome varchar(70),
@phone numeric(9),
@email varchar(50),
@ide int
as
	update agenda.professor
	set nome=@nome, phone=@phone, email=@email
	where id = @ide
go
--sp para alterar ou inserir alunos
create proc agenda.insert_aluno
@nome varchar(70),
@morada varchar(100),
@nasc date,
@problemas varchar(100) = null,
@medicacao varchar(100) = null,
@restricao varchar(100) = null,
@antipiretico varchar(100) = null
as
	if(select count(*) from agenda.aluno where nome=@nome and nasc=@nasc) = 0
		begin
			insert into agenda.aluno (nome, morada, nasc, problemas, medicacao, restricao, antipiretico)
			values (@nome, @morada, @nasc, @problemas, @medicacao, @restricao, @antipiretico)
		end
	else
		begin
			update agenda.aluno
			set nome=@nome, morada=@morada, nasc=@nasc, problemas=@problemas, medicacao=@medicacao, restricao=@restricao, antipiretico=@antipiretico
			where nome=@nome and nasc=@nasc
		end
go
--exec agenda.insert_aluno 123,123,2;
--exec agenda.insert_aluno 'João Santos', 'Rua Assunção Guimarota nº5', '2010-06-12', 'asma', 'inalador', 'gluten';
--Insert adulto
create proc agenda.insert_adulto
@nome varchar(70),
@morada varchar(100),
@phone numeric(9),
@email varchar(50) = null,
@hphone numeric(9) = null,
@wphone numeric(9) = null,
@ltrabalho varchar(100) = null,
@profissao varchar(50) = null
as
	insert into agenda.adulto
	values (@nome, @morada, @phone, @hphone, @wphone, @email, @ltrabalho, @profissao)
go
--exec agenda.insert_adulto 'Afonso Henriques', 'Rua do Castanhal Pousos', 965557014;
--atualizar adulto
create proc agenda.update_adulto
@uphone numeric(9),
@nome varchar(70),
@morada varchar(100),
@phone numeric(9),
@email varchar(50) = null,
@hphone numeric(9) = null,
@wphone numeric(9) = null,
@ltrabalho varchar(100) = null,
@profissao varchar(50) = null
as
	if @uphone=@phone
		begin
			update agenda.adulto
			set nome=@nome, morada=@morada, hphone=@hphone, wphone=@wphone, email=@email, ltrabalho=@ltrabalho, profissao=@profissao
			where phone=@uphone
		end
	else
		begin
			update agenda.adulto
			set nome=@nome, morada=@morada, phone=@phone, hphone=@hphone, wphone=@wphone, email=@email, ltrabalho=@ltrabalho, profissao=@profissao
			where phone=@uphone
		end
go
--exec agenda.update_adulto 965557014, 'Afonso Henriques', 'Rua do Castanhal Pousos', 965557014;
--inserir parentesco
create proc agenda.insert_adulto_parente
@nome varchar(70),
@nasc date,
@phone numeric(9),
@parente varchar(10)
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	if(select count(*) from agenda.parente where id_aluno = @ide and parent_phone = @phone) = 0
		begin
			insert into agenda.parente
			values (@ide, @phone,@parente)
		end
	else
		begin
			exec agenda.delete_parente @nome,@nasc,@phone
			insert into agenda.parente
			values (@ide, @phone,@parente)
		end
go
--exec agenda.insert_adulto_parente 'João Santos', '2010-06-12', 920555495, 'mãe';
--inserir levantado
create proc agenda.insert_adulto_levantado
@nome varchar(70),
@nasc date,
@phone numeric(9)
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	insert into agenda.levantado
	values (@ide, @phone)
go
--exec agenda.insert_adulto_levantado 'João Santos', '2010-06-12', 920555495;
--inserir encarregado
create proc agenda.insert_adulto_encarregado
@nome varchar(70),
@nasc date,
@phone numeric(9)
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	if(select count(*) from agenda.encarregado where id_aluno = @ide) = 0
		begin
			insert into agenda.encarregado
			values (@ide, @phone)
		end
	else
		begin
			delete from agenda.encarregado where id_aluno = @ide
			insert into agenda.encarregado
			values (@ide, @phone)
		end
go
--exec agenda.insert_adulto_encarregado 'João Santos', '2010-06-12', 920555495;
--inserir encarregado
create proc agenda.insert_adulto_encarregado
@nome varchar(70),
@nasc date,
@phone numeric(9)
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	if(select count(*) from agenda.encarregado where id_aluno = @ide) = 0
		begin
			insert into agenda.encarregado
			values (@ide, @phone)
		end
	else
		begin
			delete from agenda.encarregado where id_aluno = @ide
			insert into agenda.encarregado
			values (@ide, @phone)
		end
go
--exec agenda.insert_adulto_encarregado 'João Santos', '2010-06-12', 920555495;
--Insert grau
create proc agenda.insert_grau @grau int
as
	insert into agenda.grau values(@grau)
go
--exec agenda.insert_grau 2;
--Insert ano_letivo
create proc agenda.insert_ano @ano varchar(9)
as
	insert into agenda.ano values(@ano)
go
--exec agenda.insert_ano '2019/2020';
-------------------------------deletes----------------------------------------
--deletar aluno de uma atividade
create proc agenda.delete_aluno_atividade @nome varchar(70), @nasc date, @atividade varchar(30)
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	delete from agenda.tem where nome = @atividade
go
--deletar parente
create proc agenda.delete_parente @nome varchar(70), @nasc date, @phone numeric(9)
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	delete from agenda.parente where id_aluno = @ide and parent_phone = @phone
go
--deletar um aluno e todas as dependencias
create proc agenda.delete_aluno @nome varchar(70), @nasc date
as
	declare @ide int
	exec agenda.student_id @nome, @nasc, @ide OUTPUT;
	if(agenda.aluno_pt(@ide)) = 1
		begin
			delete from agenda.pertence_turma where id_aluno = @ide
		end
	if(agenda.aluno_vv(@ide)) = 1
		begin
			delete from agenda.vai where id_aluno = @ide
		end
	if(agenda.aluno_ta(@ide)) = 1
		begin
			delete from agenda.tem where id_aluno = @ide
		end
	if(agenda.aluno_tp(@ide)) = 1
		begin
			delete from agenda.parente where id_aluno = @ide
		end
	if(agenda.aluno_te(@ide)) = 1
		begin
			delete from agenda.encarregado where id_aluno = @ide
		end
	if(agenda.aluno_psl(@ide)) = 1
		begin
			delete from agenda.levantado where id_aluno = @ide
		end
	delete from agenda.aluno where nome=@nome and nasc=@nasc
go
