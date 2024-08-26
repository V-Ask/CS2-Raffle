FROM python:3

WORKDIR /server

COPY /src/raffle/ /server
COPY requirements.txt requirements.txt
RUN pip3 install -r requirements.txt
RUN uwsgi

COPY . .

CMD ["uwsgi", "--ini", "app.ini"]

EXPOSE 90