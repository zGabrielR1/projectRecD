# Setup necessário — DigiSat Insight

## Pré-requisitos
- Windows 10/11 x64.
- MongoDB da SuiteG6 instalado (binário `mongod.exe` / cliente `mongo.exe`), geralmente em `C:\Digisat\SuiteG6\MongoDB\bin`.
- Python 3.12 (somente para desenvolvimento; o executável final não precisa de Python).
- Node.js (somente para `node tests/editor-data.test.cjs`).

## Iniciar do zero (desenvolvimento)
1. Clone/baixe o projeto em `D:\Downloads\stuff\projetoIADigisat`.
2. Crie o ambiente virtual:
   ```powershell
   py -3.12 -m venv .venv
   .\.venv\Scripts\python.exe -m pip install -r requirements.txt
   ```
3. Configure a conexão MongoDB em `config.env` (ou use variáveis de ambiente):
   - `MONGO_URI=mongodb://127.0.0.1:12220/?directConnection=true&readPreference=secondaryPreferred`
   - `MONGO_DATABASE=DigisatServer`
   - `MONGO_USERNAME=root` (ou conta dedicada)
   - `MONGO_AUTH_SOURCE=admin`
   - `MONGO_PASSWORD_FILE=senhamongo.txt` (linha 2 contém a senha)
   - `DIGISAT_MODE=live`
   - `MAX_REPORT_ROWS=500`
   - `INSIGHT_USER=` / `INSIGHT_PASSWORD=` (vazio = acesso local sem autenticação do painel)
   - `INSIGHT_REQUIRE_READ_ONLY=1` para relatórios somente leitura
4. Garanta que o MongoDB da SuiteG6 esteja em execução na porta configurada.
5. Inicie a aplicação:
   ```powershell
   $env:INSIGHT_DATA_DIR="$env:TEMP\insight-data"
   .\.venv\Scripts\python.exe -m uvicorn app:app --host 127.0.0.1 --port 8765
   ```
6. Abra `http://127.0.0.1:8765`.

## Se o setup já estiver instalado
1. Confirme o MongoDB em execução.
2. Ative o ambiente: `.\.venv\Scripts\Activate.ps1`.
3. Inicie com o comando do passo 5 acima (ou execute `DigiSatInsight.exe` na release).
4. Para usar gravações, habilite edição no Banco de Dados Editor e use conta MongoDB com permissões de escrita.

## Testes
- `python -m unittest test_editor -v`
- `python -m unittest tests.test_composition -v`
- `node --check static/editor.js`
- `node tests/editor-data.test.cjs`

## Observações
- O editor e a alteração em massa exigem sessão de edição (`EDITAR NomeDoBanco`) e permissões `insert`/`update`/`remove` na conta MongoDB.
- `INSIGHT_REQUIRE_READ_ONLY` continua valendo para relatórios; o editor é um módulo administrativo com verificação própria.
- Mantenha `static/light.css` carregado por último para preservar o tema claro.