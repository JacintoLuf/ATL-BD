----------------------------------------------------------UDF----------------------------------------------------------------------------
--aluno pertence a turma
create function agenda.aluno_pt (@id int) returns int
as
	begin
		if(select count(*) from agenda.pertence_turma where id_aluno = @id) = 0
			begin
				return 0
			end
		return 1
	end
go
--aluno vai a visita
create function agenda.aluno_vv (@id int) returns int
as
	begin
		if(select count(*) from agenda.vai where id_aluno = @id) = 0
			begin
				return 0
			end
		return 1
	end
go
--aluno tem atividade
create function agenda.aluno_ta (@id int) returns int
as
	begin
		if(select count(*) from agenda.tem where id_aluno = @id) = 0
			begin
				return 0
			end
		return 1
	end
go
--aluno tem parente
create function agenda.aluno_tp (@id int) returns int
as
	begin
		if(select count(*) from agenda.parente where id_aluno = @id) = 0
			begin
				return 0
			end
		return 1
	end
go
--aluno tem encarregado
create function agenda.aluno_te (@id int) returns int
as
	begin
		if(select count(*) from agenda.encarregado where id_aluno = @id) = 0
			begin
				return 0
			end
		return 1
	end
go
--aluno pode ser levantado
create function agenda.aluno_psl (@id int) returns int
as
	begin
		if(select count(*) from agenda.levantado where id_aluno = @id) = 0
			begin
				return 0
			end
		return 1
	end
go
--parentesco adulto
create function agenda.adulto_p (@nome varchar(70), @nasc date, @phone numeric(9)) returns varchar(10)
as
	begin
		declare @res varchar(10)
		select @res = parentesco from agenda.parente join agenda.aluno on id=id_aluno where nome=@nome and nasc=@nasc and parent_phone=@phone
		return @res
	end
go
--adulto pode levantar
create function agenda.adulto_pl (@nome varchar(70), @nasc date, @phone numeric(9)) returns int
as
	begin
		if(select count(*) from agenda.levantado join agenda.aluno on id=id_aluno where nome=@nome and nasc=@nasc and parent_phone=@phone) = 0
			begin
				return 0
			end
		return 1
	end
go
--adulto é encarregado
create function agenda.adulto_e (@nome varchar(70), @nasc date, @phone numeric(9)) returns int
as
	begin
		if(select count(*) from agenda.encarregado join agenda.aluno on id=id_aluno where nome=@nome and nasc=@nasc and parent_phone=@phone) = 0
			begin
				return 0
			end
		return 1
	end
go
