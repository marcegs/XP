# Projeto XP:

---

## Como executar:
clone o repositório e rode via docker:
```
git clone https://github.com/marcegs/XP.git
cd XP
docker compose build
docker compose run 
```
---

## Como utilizar:

### O sistema foi desenvolvido da seguinte maneira:

- Cada usuario tem uma ou mais contas.
- Produtos existem de forma independente.
- Uma conta pode "comprar" ou "vender" um produto
- Cada movimentação feita por uma conta é gerado uma "Trade"

Podemos simplificar os testes da aplicação utilizando a coleção do Postman que está localizada na raiz do projeto.

O envio de e-mails é realizado por meio de um serviço que, para fins de demonstração, está configurado para disparar a cada 5 segundos, dentro da pasta SystemDService.