run_client:
	cd frontend && npm run dev

python_reqs:
	cd backend && pip install -r requirements.txt --user

npm_reqs:
	cd frontend && npm install

install_dependencies: python_reqs npm_reqs