# Servidor de Autorização OpenID Connect
### Utilizando ASP.NET Identity e OpenIdDict

---

## 🛠️ Instruções para Gerar Certificados

### 1. Gerar a Chave Privada
Crie uma chave privada usando o comando abaixo:
```powershell
openssl genpkey -algorithm RSA -out private_key.pem -pkeyopt rsa_keygen_bits:2048
```

### 2. Gerar um Certificado Autoassinado para Assinatura
Para gerar um certificado autoassinado que será utilizado para assinaturas:
```powershell
openssl req -new -x509 -key private_key.pem -out signing_certificate.pem -days 365
```

### 3. Gerar um Certificado Autoassinado para Criptografia
Use o comando abaixo para criar um certificado que será utilizado para criptografia:
```powershell
openssl req -new -x509 -key private_key.pem -out encryption_certificate.pem -days 365 
```

### 4. Verificar os Certificados
Confirme o conteúdo dos certificados gerados:
- Verificar o certificado de assinatura:
  ```powershell
  openssl x509 -in signing_certificate.pem -text -noout 
  ```
- Verificar o certificado de criptografia:
  ```powershell
  openssl x509 -in encryption_certificate.pem -text -noout 
  ```

### 5. Converter os Certificados para .pfx
Caso seja necessário converter os certificados para o formato `.pfx`:

- Converter o certificado de assinatura:
  ```powershell
  openssl pkcs12 -export -out signing_certificate.pfx -inkey private_key.pem -in signing_certificate.pem 
  ```
  
- Converter o certificado de criptografia:
  ```powershell
  openssl pkcs12 -export -out encryption_certificate.pfx -inkey private_key.pem -in encryption_certificate.pem 
  ```

---

📝 **Observação**: Certificados autoassinados são úteis em ambientes de teste. Para ambientes de produção, é recomendável obter certificados de uma Autoridade Certificadora (CA) confiável.