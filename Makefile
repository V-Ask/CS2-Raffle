run_server:
	cd backend && export FLASK_APP=src/raffle/app.py && flask run

run_client:
	cd frontend && npm run dev

launch_website:
	make run_server & make run_client

npm_reqs:
	cd frontend && npm install

tmux:
	tmux
	tmux send-key C-b
	tmux send-key %