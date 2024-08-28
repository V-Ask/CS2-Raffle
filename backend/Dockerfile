FROM python:3
WORKDIR /app
COPY ./requirements.txt requirements.txt
RUN pip install --no-cache-dir --upgrade -r requirements.txt
COPY . .
EXPOSE 5000
CMD ["gunicorn", "--chdir", "src", "--bind", "0.0.0.0:5000", "wsgi:app"]