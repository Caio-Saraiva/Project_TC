import os
import psycopg2
import json
from decimal import Decimal
from datetime import datetime, date

# Função auxiliar para serializar tipos não suportados (como Decimal e datetime)
def json_serial(obj):
    if isinstance(obj, Decimal):
        return round(float(obj), 2)  # Retorna como float com duas casas decimais
    if isinstance(obj, (datetime, date)):
        return obj.isoformat()  # Converter datetime/date para string no formato ISO
    raise TypeError(f"Type {type(obj)} not serializable")

# Função para inserir um novo pedido e itens de pedido
def insert_pedido_and_items(cod_cliente, data_pedido, valor_pedido, items):
    db_host = os.getenv('DB_HOST', 'seu-endereco-do-banco')
    db_name = os.getenv('DB_NAME', 'nome-do-banco')
    db_user = os.getenv('DB_USER', 'usuario-do-banco')
    db_password = os.getenv('DB_PASSWORD', 'senha-do-banco')
    db_port = os.getenv('DB_PORT', '5432')

    try:
        connection = psycopg2.connect(
            host=db_host,
            database=db_name,
            user=db_user,
            password=db_password,
            port=db_port
        )
        cursor = connection.cursor()

        # Inserir o novo pedido na tabela "pedido" e retornar o ID
        pedido_query = """
        INSERT INTO pedido (cod_cliente, data_pedido, valor_pedido)
        VALUES (%s, %s, %s) RETURNING cod_pedido;
        """
        cursor.execute(pedido_query, (cod_cliente, data_pedido, valor_pedido))
        cod_pedido = cursor.fetchone()[0]  # Recupera o ID do pedido recém-criado

        # Inserir os itens do pedido na tabela "item_pedido"
        item_query = """
        INSERT INTO item_pedido (cod_pedido, cod_produto, qtd_pedido, valor_item)
        VALUES (%s, %s, %s, %s);
        """
        
        for item in items:
            cod_produto = item['cod_produto']
            qtd_pedido = item['qtd_pedido']
            valor_item = item['valor_item']
            cursor.execute(item_query, (cod_pedido, cod_produto, qtd_pedido, valor_item))

        # Confirmar a transação
        connection.commit()

        # Fechar a conexão
        cursor.close()
        connection.close()

        # Retornar o ID do pedido recém-inserido com cabeçalho CORS
        return {
            'statusCode': 201,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps({'cod_pedido': cod_pedido})
        }

    except Exception as error:
        connection.rollback()  # Reverter a transação se houver um erro
        return {
            'statusCode': 500,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps({'error': str(error)})
        }

# Função para buscar todos os produtos
def get_all_products():
    db_host = os.getenv('DB_HOST', 'seu-endereco-do-banco')
    db_name = os.getenv('DB_NAME', 'nome-do-banco')
    db_user = os.getenv('DB_USER', 'usuario-do-banco')
    db_password = os.getenv('DB_PASSWORD', 'senha-do-banco')
    db_port = os.getenv('DB_PORT', '5432')

    try:
        connection = psycopg2.connect(
            host=db_host,
            database=db_name,
            user=db_user,
            password=db_password,
            port=db_port
        )
        cursor = connection.cursor()

        # Executar a consulta
        query = "SELECT * FROM produto"
        cursor.execute(query)

        # Pegar os resultados
        records = cursor.fetchall()
        column_names = [desc[0] for desc in cursor.description]  # Obter os nomes das colunas

        # Criar uma lista de dicionários
        result = [dict(zip(column_names, record)) for record in records]

        # Fechar a conexão
        cursor.close()
        connection.close()

        return {
            'statusCode': 200,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps(result, default=json_serial)
        }

    except Exception as error:
        return {
            'statusCode': 500,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps({'error': str(error)})
        }

# Função para buscar o último pedido
def get_last_order():
    db_host = os.getenv('DB_HOST', 'seu-endereco-do-banco')
    db_name = os.getenv('DB_NAME', 'nome-do-banco')
    db_user = os.getenv('DB_USER', 'usuario-do-banco')
    db_password = os.getenv('DB_PASSWORD', 'senha-do-banco')
    db_port = os.getenv('DB_PORT', '5432')

    try:
        connection = psycopg2.connect(
            host=db_host,
            database=db_name,
            user=db_user,
            password=db_password,
            port=db_port
        )
        cursor = connection.cursor()

        # Executar a consulta para buscar o último pedido
        query = "SELECT * FROM pedido ORDER BY cod_pedido DESC LIMIT 1"
        cursor.execute(query)

        # Pegar o resultado (somente o último pedido)
        record = cursor.fetchone()
        column_names = [desc[0] for desc in cursor.description]  # Obter os nomes das colunas

        # Criar o dicionário com os resultados
        result = dict(zip(column_names, record))

        # Fechar a conexão
        cursor.close()
        connection.close()

        return {
            'statusCode': 200,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps(result, default=json_serial)
        }

    except Exception as error:
        return {
            'statusCode': 500,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps({'error': str(error)})
        }

# Função para buscar itens de um pedido na tabela item_pedido com base no cod_pedido
def get_items_by_cod_pedido(cod_pedido):
    db_host = os.getenv('DB_HOST', 'seu-endereco-do-banco')
    db_name = os.getenv('DB_NAME', 'nome-do-banco')
    db_user = os.getenv('DB_USER', 'usuario-do-banco')
    db_password = os.getenv('DB_PASSWORD', 'senha-do-banco')
    db_port = os.getenv('DB_PORT', '5432')

    try:
        connection = psycopg2.connect(
            host=db_host,
            database=db_name,
            user=db_user,
            password=db_password,
            port=db_port
        )
        cursor = connection.cursor()

        # Executar a consulta para buscar todos os itens associados ao cod_pedido
        query = """
        SELECT * FROM item_pedido WHERE cod_pedido = %s
        """
        cursor.execute(query, (cod_pedido,))

        # Pegar os resultados
        records = cursor.fetchall()
        column_names = [desc[0] for desc in cursor.description]  # Obter os nomes das colunas

        # Criar uma lista de dicionários
        result = [dict(zip(column_names, record)) for record in records]

        # Fechar a conexão
        cursor.close()
        connection.close()

        return {
            'statusCode': 200,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps(result, default=json_serial)
        }

    except Exception as error:
        return {
            'statusCode': 500,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps({'error': str(error)})
        }

# Função Lambda Handler principal
def lambda_handler(event, context):
    # Roteamento baseado no evento recebido
    path = event.get('path', '/')
    http_method = event.get('httpMethod', 'GET')

    if path == '/pedido' and http_method == 'POST':
        # Capturar os parâmetros do corpo ou da query string
        body = json.loads(event['body']) if 'body' in event else {}
        cod_cliente = body.get('cod_cliente')
        data_pedido = body.get('data_pedido')
        valor_pedido = body.get('valor_pedido')
        items = body.get('items')  # Lista de itens do pedido

        if cod_cliente is None or data_pedido is None or valor_pedido is None or items is None:
            return {
                'statusCode': 400,
                'headers': {
                    'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                    'Content-Type': 'application/json'
                },
                'body': json.dumps({'error': 'Parâmetros insuficientes'})
            }

        # Inserir o pedido e os itens no banco de dados
        return insert_pedido_and_items(cod_cliente, data_pedido, valor_pedido, items)

    elif path == '/produtos' and http_method == 'GET':
        # Buscar todos os produtos
        return get_all_products()

    elif path == '/ultimo-pedido' and http_method == 'GET':
        # Buscar o último pedido
        return get_last_order()

    elif path == '/item-pedido' and http_method == 'GET':
        # Capturar o valor de cod_pedido da query string
        query_params = event.get('queryStringParameters', {})
        cod_pedido = query_params.get('cod_pedido')

        if cod_pedido is None:
            return {
                'statusCode': 400,
                'headers': {
                    'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                    'Content-Type': 'application/json'
                },
                'body': json.dumps({'error': 'Parâmetro cod_pedido ausente'})
            }

        # Buscar os itens com base no cod_pedido
        return get_items_by_cod_pedido(cod_pedido)

    else:
        return {
            'statusCode': 404,
            'headers': {
                'Access-Control-Allow-Origin': '*',  # Cabeçalho CORS
                'Content-Type': 'application/json'
            },
            'body': json.dumps({'error': 'Rota não encontrada'})
        }
